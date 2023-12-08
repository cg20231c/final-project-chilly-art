using System;

using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class Controller : MonoBehaviour
{
    // Having data sent and recieved in a seperate thread to the main game thread stops unity from freezing
    Thread IOThread = new Thread(DataThread);
    private static SerialPort sp; 
    // If the serial port class does not exist open your NuGet package manager Project->Manage NuGet Packages->Browse and search for
    // Serial Port. Install System.IO.Ports by Microsoft
    
    // Stores any data that comes in via the serial port
    private static string incomingMsg = "";
    // Stores the data to be sent to the arduino via the serial port
    private static string outgoingMsg = "";

    private float gyroX, gyroY, gyroZ;

    // Camera rotation speed (adjust as needed)
    public float rotationSpeed = 0.00000000000001f;


    private static void DataThread()
    {
        // Opens the serial port for reading and writing data
        sp = new SerialPort("COM5", 115200); // Alter the first value to be whatever port the arduino is connected to within the arduino IDE; Alter the second value to be the same as Serial.beign at the start of the arduino program
        sp.Open();

        // Every 200ms, it checks if there is a message stores in the output buffer string to be sent to the arduino,
        // Then recieves any data being sent to the project via the arduino 
        while(true)
        {
            if (outgoingMsg != "")
            {
                sp.Write(outgoingMsg);
                outgoingMsg = "";
            }

            incomingMsg = sp.ReadExisting();
            Thread.Sleep(10);
        }
    }

    private void OnDestroy()
    {
        // Closes the thread and serial port when the game ends
        IOThread.Abort();
        sp.Close();
    }

    // Start is called before the first frame update
    void Start()
    {
        IOThread.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (incomingMsg != "")
        {
            // Split the incoming data into lines
            string[] lines = incomingMsg.Split('\n');

            foreach (string line in lines)
            {
                // Split each line into X, Y, Z values
                string[] values = line.Split(' ');

                if (values.Length == 6 && values[0] == "X:" && values[2] == "Y:" && values[4] == "Z:")
                {
                    float.TryParse(values[1], out gyroX);
                    float.TryParse(values[3], out gyroY);
                    float.TryParse(values[5], out gyroZ);
                    Debug.Log("Received Gyro Data - X: " + gyroX + " Y: " + gyroY + " Z: " + gyroZ);
                    // Adjust the camera's rotation based on gyro values
                    RotateCamera();
                }
            }

            incomingMsg = "";
        }

    void RotateCamera()
    {
        // Adjust camera rotation based on gyro values
        float rotationX = -gyroY * 0;
        float rotationY = -gyroX * 0.05f;
        float rotationZ = gyroZ * 0.01f;

        // Apply rotation to the camera
        transform.Rotate(rotationZ, rotationY, rotationX);
    }

    }
}