using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XCENT.JobServer.Abstract;
using XCENT.JobServer.Plugin;

namespace ModuleProject
{

    public enum Colors
    {
        Red, 
        Orange, 
        Yellow,
        Green,
        Blue, 
        Indigo, 
        Violet
    }

    [Flags]
    public enum Days
    {
        Sunday, 
        Monday, 
        Tuesday, 
        Wednesday,
        Thursday,
        Friday, 
        Saturday
    }



    public class WebModule : ModuleBase
    {
        public override string Description { get { return "Test module for proper handling of required parameters with defaults."; } }
        public override string InfoURL { get { return "http:\\jobserver.net"; } }

        [ParamDef(Caption: "Color", Description: "Color choice", ModuleParameterDirection: ModuleParameterDirection.In, IsRequired:true, Default:"Blue")]
        public Colors Color { get; set; }

        [ParamDef(Caption: "Day", Description: "Day Choice", ModuleParameterDirection: ModuleParameterDirection.In, IsRequired:true, Default:"Monday, Tuesday, Wednesday" )]
        public Days Days { get; set; }

        [ParamDef( Caption: "Wait", Description: "Wait (seconds)", ModuleParameterDirection: ModuleParameterDirection.In, IsRequired: true, Default: "15", MinValue: 0, MaxValue: 120 )]
        public int Wait { get; set; }

        [ParamDef(Caption: "Color Days", Description: "Color Days", ModuleParameterDirection: ModuleParameterDirection.Out )]
        public string  ColorDays { get; set; }


        public WebModule() : base("WebColorModule", "WEB", "Color Day Module", null, Guid.Parse("298D7FB6-0C57-4023-89BB-79D7BF641BAC"))
        { // null means that there is no limit to concurrently running modules; replace with number to set limit
        }

        public override ModuleRunResult OnRun()
        {
            var msg = $"{Color} {Days}";

            ColorDays = msg;
            SetMessage(msg);
            System.Threading.Thread.Sleep(Wait*1000);
            return new ModuleRunResult() { Outcome = ExecutionOutcome.Success };
        }

        public override bool Stop()
        {
            // Return true to indicate that the process will stop on it's own
            return true;
        }
    }
}
