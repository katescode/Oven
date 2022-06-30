using System;
using System.Timers;

namespace MyApplication
{


    class Oven
    {

        public int current_temperature;
        public int desired_temperature;
        public int test_temp = 0;
        public int oven_time = 0;
        public bool has_error;
        public bool oven_on;
        public int[] probes = { 0, 0 };

        public void Broil_Coil_ON()
        {
            Console.WriteLine("Broil Coil on");
        }

        public void Bake_Coil_ON()
        {
            Console.WriteLine("Bake Coil on");
        }

        public void Broil_Coil_OFF()
        {
            Console.WriteLine("Broil Coil is off");
        }

        public void Bake_Coil_OFF()
        {
            Console.WriteLine("Bake Coil is off");
        }

        public void Beep()
        {
            Console.WriteLine("BEEEEEP");
        }

        public void Oven_On()
        {
            oven_on = true;
            Console.WriteLine("Oven is on");
        }

        public void Oven_Off()
        {
            Console.WriteLine("Oven is off");
        }

        // Creates a random temperatures to insert into Update_Probes()
       
       public int Probe_Input()
        {
            Random probe_input = new Random();
            return probe_input.Next(300, 400);

        }
       
       // Check_Temp() will error if Temperature readings are not within 10 degrees of each other.
        // For testing purposes, the probes will not update if the random number inputs are not within 10. 
       
       public void Update_Probes()
        {
            Console.WriteLine("updating probes");
            bool numbers_good = false;
            while (numbers_good == false)
        
            {
                probes[0] = Probe_Input();
                probes[1] = Probe_Input();
                int probe_test = probes[0] - probes[1];
                if (probe_test <= 10 && probe_test >= -10)
                {
                    numbers_good = true;
                }
            }

        }

        // Check_Temp() will insert the probe readings, determine if they are within 10 degrees, and then average the probe readings.
        
        public int Check_Temp()
        {
            Update_Probes();
            Array.Sort(probes);
            if (probes[1] - probes[0] <= 10)
            {
                current_temperature = (probes[1] + probes[0]) / 2;
                Console.WriteLine($"The current temp is {current_temperature}");
                return current_temperature;
            }
            else
            {
                Console.WriteLine("ERROR");
                has_error = true;
                return 0;
            }
        }

        // Preheat_Oven will test the temperature readings, and turn on both coils until desired temperature is reached.
        
        public void Preheat_Oven()
        {
            Check_Temp();
            current_temperature = test_temp;
            Broil_Coil_ON();
            Bake_Coil_ON();
            Thread.Sleep(20000);
            Check_Temp();
            if (test_temp == current_temperature)
            {
                Broil_Coil_OFF();
                Bake_Coil_OFF();
                Console.WriteLine("Error");
            }
            else
                while (current_temperature < desired_temperature)
                {
                    Thread.Sleep(5000);
                    Check_Temp();
                }
            Broil_Coil_OFF();
            Bake_Coil_OFF();
        }

        // Maintain_Heat() will keep the oven heated to the desired temperature until the oven is manually turned off.
       
       public void Maintain_Heat()
        {
            while (oven_on == true)
            {
                if (current_temperature < desired_temperature)
                {
                    Bake_Coil_ON();
                    Thread.Sleep(5000);
                    Check_Temp();
                }
                else
                {
                    Bake_Coil_OFF();
                    Check_Temp();
                }
            }
        }
    }

    class My_Oven
    {
        public static void Main()

        {
            Oven Oven1 = new Oven();
            


            Console.WriteLine("Enter Temperature:");
            Oven1.desired_temperature = Convert.ToInt32(Console.ReadLine());
            Oven1.Oven_On();

            Oven1.Preheat_Oven();
            Oven1.Beep();
            Oven1.Maintain_Heat();
        }
    }
}


