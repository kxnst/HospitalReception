using System;
using System.Collections.Generic;

namespace HospitalReception.Models
{
    public class AllowedTimeRenderer
    {
        public const int step = 20;
        public const int start = 9;
        public const int end = 19;

        public List<DateTime> RenderAllowedTime(DateTime[] exclude, DateTime current)
        {
            DateTime startTime = new DateTime(current.Year, current.Month, current.Day, start, 0, 0);
            List<DateTime> times = new List<DateTime>();
            while (startTime.Hour != end)
            {
                startTime = startTime.AddMinutes(step);
                {
                    try
                    {
                        foreach (DateTime time in exclude)
                        {

                            if (time.Hour == startTime.Hour && time.Minute == startTime.Minute)
                            {
                                throw new Exception();
                            }
                        }
                        times.Add(new DateTime(startTime.Year, startTime.Month, startTime.Day, startTime.Hour, startTime.Minute, 0));

                    }
                    catch (Exception) {
                        continue;
                    }

                }
            }
            return times;
        }
    }
}
