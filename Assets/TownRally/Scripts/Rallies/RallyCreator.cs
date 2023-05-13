using System.Collections.Generic;
using static TownRally.Rally;

namespace TownRally
{
    internal class RallyCreator
    {
        internal enum NewRallyType
        {
            None = 0,
            RallyGrazMur = 1,
            RallyGrazSchlossberg = 2,
            RallyOedWald = 3,
        }

        internal static EventIn_AddNewRallyToServer EventIn_AddNewRallyToServer = new EventIn_AddNewRallyToServer();

        private static readonly string TXT_IDPREFIX_HOST = "host";
        private static readonly string TXT_IDPREFIX_USER = "user";
        private static readonly string TXT_RALLY_ID = "&1_&2";
        private static readonly string TXT_STATION_ID = "&1_&2";
        private static readonly string TXT_TASK_ID = "&1_&2";

        private Dictionary<string, Station> stations = new Dictionary<string, Station>();
        private Dictionary<string, RallyTask> tasks = new Dictionary<string, RallyTask>();
        private Rally rally = new Rally();
        private List<Description> rallyDescription = new List<Description>();
        private int taskCreationCounter = 0;

        internal void Init()
        {
            EventIn_AddNewRallyToServer.AddListenerSingle(AddNewRallyToServer);
        }

        private void AddNewRallyToServer(NewRallyType rallyType)
        {
            if (rallyType == NewRallyType.RallyGrazMur) { this.CreateRallyGrazMur(); }
            if (rallyType == NewRallyType.RallyGrazSchlossberg) { this.CreateRallyGrazSchlossberg(); }
            if (rallyType == NewRallyType.RallyOedWald) { this.CreateRallyOedWald(); }
        }

        private void CreateRallyGrazMur()
        {
            this.rally = new Rally();
            this.stations.Clear();
            this.tasks.Clear();
            this.rallyDescription.Clear();

            // rally
            this.rallyDescription.Add(new Description { Type = DescriptionType.Image, Data = "descr_00.jpg" });
            this.rallyDescription.Add(new Description { Type = DescriptionType.Text, Data = "In der Stadt und auf der Höh’ – das schließt sich in Graz keineswegs aus. Der Fluss, die einzigartige Altstadt und mitten drin ein Berg. Der Grazer Schlossberg ist Sehenswürdigkeit, Naturschauspiel, Naherholungsgebiet und Aussichtspunkt zugleich. In kürzester Zeit gelangt man nach oben und genießt den herrlichen Ausblick auf Graz, die Altstadt und Umgebung. Einer Burg, die vor über 1.000 Jahren auf dem Schlossberg errichtet wurde, verdankt die Stadt ihren Namen. Aus dem slawischen Gradec für ,kleine Burg‘ wurde später Graz.\r\n\r\nSeit 1894 überwindet die Schlossbergbahn bravourös die rund 60% Steigung hinauf zum Schlossbergplateau - mit modernen Panoramagondeln.\r\n\r\nEine schnellere Art den Schlossberg zu erreichen, ist die Fahrt mit dem Schlossberglift." });
            this.rallyDescription.Add(new Description { Type = DescriptionType.Image, Data = "descr_00.jpg" });
            this.rallyDescription.Add(new Description { Type = DescriptionType.Text, Data = "In der Stadt und auf der Höh’ – das schließt sich in Graz keineswegs aus. Der Fluss, die einzigartige Altstadt und mitten drin ein Berg. Der Grazer Schlossberg ist Sehenswürdigkeit, Naturschauspiel, Naherholungsgebiet und Aussichtspunkt zugleich. In kürzester Zeit gelangt man nach oben und genießt den herrlichen Ausblick auf Graz, die Altstadt und Umgebung. Einer Burg, die vor über 1.000 Jahren auf dem Schlossberg errichtet wurde, verdankt die Stadt ihren Namen. Aus dem slawischen Gradec für ,kleine Burg‘ wurde später Graz.\r\n\r\nSeit 1894 überwindet die Schlossbergbahn bravourös die rund 60% Steigung hinauf zum Schlossbergplateau - mit modernen Panoramagondeln.\r\n\r\nEine schnellere Art den Schlossberg zu erreichen, ist die Fahrt mit dem Schlossberglift." });
            string rallyID = CreateRally("grazmur1", "MurRally", "Entdecke die Mur in Graz", this.rallyDescription.ToArray(), new KeyValuePair<float, float>(47.070302f, 15.435040f));

            string stationID = string.Empty;
            // station 1
            stationID = this.CreateStation(rallyID, 41.123f, 15.321f, new int[1] { 2 });
            // tasks
            this.taskCreationCounter = 0;
            this.CreateTask(stationID, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Hop hop, auf zum Mur!" } });
            this.CreateTask(stationID, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Hop hop, auf zum Mur! Zum zweiten Mal!" } });

            // station 2
            stationID = this.CreateStation(rallyID, 41.223f, 15.321f, new int[0]);
            // tasks
            this.taskCreationCounter = 0;
            this.CreateTask(stationID, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Hop hop, auf zum Mur, Station 2!" } });
            this.CreateTask(stationID, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Hop hop, auf zum Mur, Station 2! Zum zweiten Mal!" } });

            DatabaseHandler.EventIn_SaveRallyWhole.Invoke(rallyID, rally,
                new Dictionary<string, Station>(this.stations), new Dictionary<string, RallyTask>(this.tasks));
        }

        private void CreateRallyGrazSchlossberg()
        {
            this.rally = new Rally();
            this.stations.Clear();
            this.tasks.Clear();
            this.rallyDescription.Clear();

            // rally
            this.rallyDescription.Add(new Description { Type = DescriptionType.Image, Data = "descr_00.jpg" });
            this.rallyDescription.Add(new Description { Type = DescriptionType.Text, Data = "Der Schloßberg in Graz bildet mit 123 Metern Höhe, ausgehend vom Grazer Hauptplatz, den höchsten natürlichen Punkt der Stadt und bietet einen 360° Rundblick über die Stadt Graz und deren Grenzen hinaus. Beginnen wir mit der Geschichte des Schloßbergs. Im 12. Jahrhundert wurde auf dem Schloßberg eine Burg errichtet, die der Stadt Graz auch ihren Namen gab. Einer Ableitung aus „gradec“ – dem slowenischen Begriff für kleine Burg. Da die Burg nie erobert wurde, ist sie im Guinness Buch der Rekorde als die stärkste Festung aller Zeiten aufgelistet. Nicht einmal Napoleon schaffte es im 19. Jahrhundert die Burg einzunehmen. Erst als er durch die Besetzung Wiens 1809 Graz erpresste, Wien zu zerstören, ergab sich die Stadt Graz. Bis auf den Glockenturm und den Uhrturm, die von den Grazern freigekauft wurden, wurde die Burg im Großen und Ganzen abgetragen und gesprengt, eine sogenannte Schleifung. 30 Jahre später legte Ludwig Freiherr von Weldenman Spazierwege und einen romantischen Garten am Schloßberg an." });
            string rallyID = CreateRally("grazschlossb1", "Schloßberg Rally", "Ausblick inmitten der Altstadt", this.rallyDescription.ToArray(), new KeyValuePair<float, float>(47.075864f, 15.437085f));

            string stationID = string.Empty;
            // station 1
            stationID = this.CreateStation(rallyID, 47.073826f, 15.440524f, new int[1] { 2 });
            // tasks
            this.taskCreationCounter = 0;
            this.CreateTask(stationID, RallyTask.Type.GotoDestination, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Begib dich zum Kameliterplatz (Karmeliterpl., 8010 Graz)" } });
            this.CreateTask(stationID, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Hallo und herzlich willkommen bei der<br><b><size=130%>Schloßberg-Townrallye!" } });
            this.CreateTask(stationID, RallyTask.Type.Game_Objectfind, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Um die Rallye zu starten, suche mit deinem Handy eine Teufelsfigur am Kamerliterplatz!" } });

            // station 2
            stationID = this.CreateStation(rallyID, 47.073671f, 15.437642f, new int[0]);
            // tasks
            this.taskCreationCounter = 0;
            this.CreateTask(stationID, RallyTask.Type.GotoDestination, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Begib dich zum Kameliterplatz (Karmeliterpl., 8010 Graz)" } });
            this.CreateTask(stationID, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Hallo und herzlich willkommen bei der<br><b><size=130%>Schloßberg-Townrallye!" } });

            DatabaseHandler.EventIn_SaveRallyWhole.Invoke(rallyID, rally,
                new Dictionary<string, Station>(this.stations), new Dictionary<string, RallyTask>(this.tasks));
        }

        private void CreateRallyOedWald()
        {
            this.rally = new Rally();
            this.stations.Clear();
            this.tasks.Clear();
            this.rallyDescription.Clear();

            // rally
            this.rallyDescription.Add(new Description { Type = DescriptionType.Image, Data = "descr_00.jpg" });
            this.rallyDescription.Add(new Description { Type = DescriptionType.Text, Data = "Beschreibung des Waldes bitte hier einfügen..." });
            this.rallyDescription.Add(new Description { Type = DescriptionType.Image, Data = "descr_01.jpg" });
            string rallyID = CreateRally("zauberwald00", "Zauberwald", "Willkommen im Zauberwald", this.rallyDescription.ToArray(), new KeyValuePair<float, float>(47.058987f, 15.868509f));

            string stationID = string.Empty;
            // station 1
            stationID = this.CreateStation(rallyID, 47.057178f, 15.868403f, new int[1] { 2 });
            // tasks
            this.taskCreationCounter = 0;
            this.CreateTask(stationID, RallyTask.Type.GotoDestination, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Begib dich zum ersten Teil des Waldstücks!" } });
            this.CreateTask(stationID, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Willkommen zum ersten Waldstück!" } });

            // station 2
            stationID = this.CreateStation(rallyID, 47.061213f, 15.866783f, new int[1] { 3 });
            // tasks
            this.taskCreationCounter = 0;
            this.CreateTask(stationID, RallyTask.Type.GotoDestination, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Gehe durch den Wald und finde den Zweiten Wegpunkt!" } });
            this.CreateTask(stationID, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Willkommen beim zweiten Waldstück!" } });
            this.CreateTask(stationID, RallyTask.Type.Game_Objectfind, new Description[1] { new Description { Type = DescriptionType.Image, Data = "game00.jpg" } });

            // station 3
            stationID = this.CreateStation(rallyID, 47.060489f, 15.868046f, new int[1] { 4 });
            // tasks
            this.taskCreationCounter = 0;
            this.CreateTask(stationID, RallyTask.Type.GotoDestination, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Ab zum nächsten wegpunkt! Jetzt geht es direkt ins Dickicht." } });
            this.CreateTask(stationID, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Hast du dich verirrt? Hier wohnt die Waldhexe!" } });

            // station 4
            stationID = this.CreateStation(rallyID, 47.057089f, 15.871939f, new int[2] { 5, 6 }, 7);
            // tasks
            this.taskCreationCounter = 0;
            this.CreateTask(stationID, RallyTask.Type.GotoDestination, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Tiefer in den Wald! Los los los!" } });
            this.CreateTask(stationID, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Auf dieser Weggabelung musst du dich entscheiden! Welchen Wegpunkt willst du zuerst aufsuchen?" } });

            // station 5
            stationID = this.CreateStation(rallyID, 47.060899f, 15.871932f, new int[0]);
            // tasks
            this.taskCreationCounter = 0;
            this.CreateTask(stationID, RallyTask.Type.GotoDestination, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Suche den tiefsten, tiefsten Wald auf!" } });
            this.CreateTask(stationID, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Die Geister tanzen hier!" } });

            // station 6
            stationID = this.CreateStation(rallyID, 47.051357f, 15.871713f, new int[0]);
            // tasks
            this.taskCreationCounter = 0;
            this.CreateTask(stationID, RallyTask.Type.GotoDestination, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Finde die Waldlichtung!" } });
            this.CreateTask(stationID, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Die Waldlichtung erleuchtet dir den Weg..." } });

            // station 7
            stationID = this.CreateStation(rallyID, 47.052431f, 15.875369f, new int[0]);
            // tasks
            this.taskCreationCounter = 0;
            this.CreateTask(stationID, RallyTask.Type.GotoDestination, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Raus aus dem Wald!" } });
            this.CreateTask(stationID, RallyTask.Type.InfoScreen, new Description[1] { new Description { Type = DescriptionType.Text, Data = "Herzlichen Glückwunsch! Du hast die Oeder Waldrunde gut überstanden." } });

            DatabaseHandler.EventIn_SaveRallyWhole.Invoke(rallyID, rally,
                new Dictionary<string, Station>(this.stations), new Dictionary<string, RallyTask>(this.tasks));
        }

        private string CreateRally(string rallyNameInternal, string rallyName, string caption, Description[] descr, KeyValuePair<float, float> position)
        {
            string rallyID = TXT_RALLY_ID.Replace("&1", TXT_IDPREFIX_HOST).Replace("&2", rallyNameInternal);
            rally.Name = rallyName;
            rally.Caption = caption;
            rally.Descr = descr;
            rally.Pos = position;
            return rallyID;
        }

        private string CreateStation(string rallyID, float posX, float posY, int[] nextStations, int finalStation = -1)
        {
            Station station = new Station();
            string stationID = TXT_STATION_ID.Replace("&1", rallyID).Replace("&2", stations.Count.ToString());
            station.Pos = new KeyValuePair<float, float>(posX, posY);
            station.NextStations = nextStations;
            station.FinalStation = finalStation;
            station.RallyID = rallyID;
            this.stations.Add(stationID, station);
            return stationID;
        }

        private void CreateTask(string stationID, RallyTask.Type taskType, Description[] descr)
        {
            RallyTask task = new RallyTask();
            string taskID = TXT_TASK_ID.Replace("&1", stationID).Replace("&2", (this.taskCreationCounter++).ToString());
            task.Descr = descr;
            task.TType = taskType;
            task.StationID = stationID;
            tasks.Add(taskID, task);
        }
    }
}
