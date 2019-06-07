namespace Roku;
        // class declarations
         class RokuTcp;
         class RokuApp;
     class RokuTcp 
    {
        // class delegates

        // class events
        EventHandler AppUpdate ( RokuTcp sender, EventArgs e );

        // class functions
        FUNCTION HttpApp ( SIGNED_LONG_INTEGER i );
        FUNCTION HttpGetApps ();
        FUNCTION HttpCall ( STRING cmd );
        FUNCTION TriggerAppUpdate ();
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        STRING BaseUrl[];
        INTEGER Appnum;
        STRING AppName[][];
        STRING AppId[][];
        STRING AppType[][];

        // class properties
    };

