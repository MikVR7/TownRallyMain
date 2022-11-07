
using System.Collections.Generic;
using System.Linq;
using TheraBytes.BetterUi;
using UnityEngine;

namespace TownRally
{
    internal class RalliesHandler
    {
        private static RalliesHandler Instance = null;
        internal static EventIn_SetCurrentRally EventIn_SetCurrentRally = new EventIn_SetCurrentRally();
        internal static EventOut_RallyChanged EventOut_RallyChanged = new EventOut_RallyChanged();
        internal static EventIn_CurrentTaskFinished EventIn_CurrentTaskFinished = new EventIn_CurrentTaskFinished();
        internal static EventOut_RallyStationTaskChanged EventOut_RallyStationTaskChanged = new EventOut_RallyStationTaskChanged();
        //internal static EventOut_RallyStationChanged EventOut_RallyStationChanged = new EventOut_RallyStationChanged();

        private List<Rally> rallies { get; set; } = new List<Rally>();
        private int currentRallyID { get; set; } = 0;

        internal static int VarOut_GetRalliesCount() { return Instance.rallies.Count; }
        internal static Rally VarOut_GetCurrentRally()
        {
            return Instance.rallies[Instance.currentRallyID];
        }
        internal static string VarOut_GetRallyNameByID(int id) { return Instance.rallies[id].Name; }
        internal static string VarOut_GetRallyCaptionByID(int id) { return Instance.rallies[id].Caption; }
        internal static RallyStationTask VarOut_GetCurrentTask()
        {
            Rally rally = Instance.rallies[Instance.currentRallyID];
            RallyStation rallyStation = rally.Stations[rally.currentStation];
            RallyStationTask task = rallyStation.Tasks[rallyStation.currentTask];
            return task;
        }

        internal void Init()
        {
            Instance = this;
            EventIn_SetCurrentRally.AddListener(OnSetCurrentRally);
            EventIn_CurrentTaskFinished.AddListenerSingle(CurrentTaskFinished);
            this.CreateAllRallies();
        }

        private void CreateAllRallies()
        {
            this.rallies.Clear();

            Rally rally1 = new Rally();
            //rally1.ID = this.rallies.Count;
            rally1.Name = "MurRally";
            rally1.Caption = "Entdecke die Mur in Graz";
            rally1.Description = new List<string>() {
                "IMG:Rallies/SchlossbergRally/sbg_main.jpg",
                "TXT:In der Stadt und auf der H�h� � das schlie�t sich in Graz keineswegs aus. Der Fluss, die einzigartige Altstadt und mitten drin ein Berg. Der Grazer Schlossberg ist Sehensw�rdigkeit, Naturschauspiel, Naherholungsgebiet und Aussichtspunkt zugleich. In k�rzester Zeit gelangt man nach oben und genie�t den herrlichen Ausblick auf Graz, die Altstadt und Umgebung. Einer Burg, die vor �ber 1.000 Jahren auf dem Schlossberg errichtet wurde, verdankt die Stadt ihren Namen. Aus dem slawischen Gradec f�r ,kleine Burg� wurde sp�ter Graz.\r\n\r\nSeit 1894 �berwindet die Schlossbergbahn bravour�s die rund 60% Steigung hinauf zum Schlossbergplateau - mit modernen Panoramagondeln.\r\n\r\nEine schnellere Art den Schlossberg zu erreichen, ist die Fahrt mit dem Schlossberglift.",
                "IMG:Rallies/SchlossbergRally/sbg_main.jpg",
                "TXT:In der Stadt und auf der H�h� � das schlie�t sich in Graz keineswegs aus. Der Fluss, die einzigartige Altstadt und mitten drin ein Berg. Der Grazer Schlossberg ist Sehensw�rdigkeit, Naturschauspiel, Naherholungsgebiet und Aussichtspunkt zugleich. In k�rzester Zeit gelangt man nach oben und genie�t den herrlichen Ausblick auf Graz, die Altstadt und Umgebung. Einer Burg, die vor �ber 1.000 Jahren auf dem Schlossberg errichtet wurde, verdankt die Stadt ihren Namen. Aus dem slawischen Gradec f�r ,kleine Burg� wurde sp�ter Graz.\r\n\r\nSeit 1894 �berwindet die Schlossbergbahn bravour�s die rund 60% Steigung hinauf zum Schlossbergplateau - mit modernen Panoramagondeln.\r\n\r\nEine schnellere Art den Schlossberg zu erreichen, ist die Fahrt mit dem Schlossberglift."};
            rally1.Stations = new Dictionary<int, RallyStation>()
            {
                {
                    0,
                    new RallyStation() {
                        //ID = 0,
                        //Name = "Ankunft an der Mur",
                        //Description = "Die Mur entspringt in Salzburg, genauer gesagt in Muhr im Lungau. Die Mur durchflie�t drei L�nder: �sterreich, Slowenien, Ungarn und Kroatien, an manchen Stellen dient sie sogar als Landesgrenze. So erscheint es kaum verwunderlich, dass die Mur noch einen weiteren Namen tr�gt. Im Slowenischen, Kroatischen sowie Ungarischen wird sie Mura genannt. Mehrere Fl�sse verbinden sich mit der Mur, so m�ndet in Kroatien der Fluss Trnava in der n�rdlichen kroatischen Landschaft Me?imurje, was �bersetzt  auch �Zwischen der Mur� oder �Murinsel� bedeutet. Auch der gro�e Nebenfluss zur Donau, die Drau, m�ndet in die Mur. Die beiden gro�en Fl�sse treffen sich auch im n�rdlichen Kroatien, in der N�he der Ortschaft Legrad. Nach insgesamt 430km hat die Mur ihr Ziel erreicht: das Schwarze Meer. (Karte: Begriff Binnenmeer)\r\nDie Mur bildet den Hauptfluss der Steiermark, vor allem f�r die Gr�ndung von St�dten und D�rfern spielte sie in der Vergangenheit eine wichtige Rolle, schlie�lich bietet die N�he zu flie�enden Gew�ssern den Menschen viele Vorteile: frisches Trinkwasser, Fischfang und Handelswege w�ren drei davon. In der Steiermark tragen manche Ortschaften den Namen �Mur� sogar in ihrer Bezeichnung wie zum Beispiel Bruck an der Mur, Murau, und Mureck.",
                        Tasks= new List<RallyStationTask>()
                        {
                            new RallyStationTask()
                            {
                                TaskType = RallyStationTask.Type.InfoScreen,
                            },
                            new RallyStationTask()
                            {
                                TaskType = RallyStationTask.Type.GotoDestination,
                                Description = "Hop hop, auf zum Uhrturm!"
                            },
                        }
                    }
                },
                {
                    1,
                    new RallyStation() {
                        //ID = 1,
                        //Name = "Murinsel",
                        //Description = "Wer sagt, dass Graz keine Insel besitzt, der war wohl noch nie auf der ber�hmten Murinsel! \r\nSeit dem Jahr 2003, als Graz Kulturhauptstadt Europas war, gibt es die schwimmende Insel auf der Mur. \r\nDie Idee f�r Die Insel in der Mur stammt von dem geb�rtigen Grazer Robert Punkenhofer und seiner Firma Art & Idea. Umgesetzt wurde das Projekt Murinsel von Vito Acconci, einem New Yorker K�nstler. \r\nOptisch erinnert die Form der Murinsel an eine Muschel. Jede Muschelfl�che erf�llt dabei eine eigene Funktion, wie das Amphitheater, das Cafe, oder Spielplatz am Dach. \r\nDer K�nstler Acconci selbst beschreibt die Murinsel mit den Worten: ��das Wasser rund um diese Insel flie�t und bewegt sich st�ndig, und wir wollten etwas konstruieren, das ebenfalls fl�ssig und ver�nderlich ist.\"\r\nBetrachtet man die Materialien genauer f�llt auf, dass die Grazer H�gellandschaft mit den zu H�geln geformten Edelstahlelementen dargestellt wurde. Die Konstruktion der Insel selbst besteht aus einem Stahlskelett, auch die Stege, die die Insel mit den Ufern verbinden, bestehen aus Stahl. Der asphaltierte Gehweg erm�glicht durch eine Beheizung auch im Winter eine rutschfreie Begehung. \r\nDie Mur trennt Graz in zwei Teile und die Murinsel stellt seit 2003 eine wichtige Verbindung dieser beiden Stadtteile dar. Ihre Stege verbinden den Mariahilferplatz mit dem Schlo�bergplatz. \r\nDie Grazer Murinsel ist 50 Meter lang und 20 Meter breit, mit ihren 450 Tonnen ist sie zwar nicht gerade leicht, jedoch h�lt sie auch st�rmischen Regenf�llen und damit verbunden hohem Wasserstand auf der Mur stand. Die Kosten f�r dieses Bauprojekt betrugen ca. 5,75 Millionen �. \r\n",
                        Tasks= new List<RallyStationTask>()
                        {
                            new RallyStationTask()
                            {
                                TaskType = RallyStationTask.Type.InfoScreen
                            },
                            new RallyStationTask()
                            {
                                TaskType = RallyStationTask.Type.GotoDestination,
                                Description = "Hop hop, auf zum Uhrturm!"
                            },
                        }
                    }
                },
                {
                    2,
                    new RallyStation() {
                        Tasks= new List<RallyStationTask>()
                        {
                            new RallyStationTask()
                            {
                                TaskType = RallyStationTask.Type.InfoScreen
                            },
                            new RallyStationTask()
                            {
                                TaskType = RallyStationTask.Type.GotoDestination,
                                Description = "Hop hop, auf zum Uhrturm!"
                            },
                        },
                        //ID = 2,
                        //Name = "Murnockerl",
                        //Description = "Runde Steine mit bewegter Geschichte\r\nMurnockerl sind Dokumente der letzten Eiszeit in unseren Alpen und bilden �ber weite Fl�chen den geologischen Untergrund der Stadt Graz und der s�dlich anschlie�enden Verebnungen. T�glich marschieren wir also �ber tausende von Murnockerln. Die runden Steine sind ein bedeutender Grundwasserk�rper im Steirischen Becken und Massenrohstoffe. Jeder �sterreicher verbraucht im Durchschnitt 12 Tonnen mineralische Rohstoffe pro Jahr, ein Gro�teil davon wird verbaut.\r\nFarbe, Form und Gr��e der Murnockerln erz�hlen uns von ihrer Zusammensetzung, ihrem Herkunftsort und ihrem meist turbulenten Transportweg im Wasser. Ein Gesteinsanschliff zeigt uns Strukturen und den Internaufbau der gerundeten Gesteine. Bei Betrachtung eines Gesteinsd�nnschliffes im Mikroskop sehen wir die farbenpr�chtige mineralische Zusammensetzung der �grauen Steine\".\r\n\r\nGesteine sind das Archiv unserer Erde, sie erz�hlen (Erd-)Geschichten: �ber die Ver�nderung der Landschaft, des Klimas, des Lebens auf unserer Erde, letztlich: auch �ber uns selbst. In der Langen Nacht der Forschung informieren wir �ber die Bedeutung und den Informationsgehalt der runden Steine und Sie haben die M�glichkeit, mittels eines Polarisationsmikroskops durch ein Gestein zu schauen.https://www.museum-joanneum.at/naturkundemuseum/ihr-besuch/programm/goe-paleo/events/event/2808/was-sind-eigentlich-murnockerl \r\nDie Maria Theresien Allee als Verkehrsweg l�sst sich bis ins Mittelalter zur�ckverfolgen. Vom Paulustor zum Geidorfplatz wird diese Allee heute auf fast der gesamten L�nge von einem Gehweg mit einer Mosaiksteinoberfl�che aus kleinen Flusssteinen, den sog. \"Murnockerln\", wie von einem ausgerollten Teppich begleitet. Dieses Grazer Kieselornamentband ist einzigartig in Graz und �sterreich.\r\n\r\n",
                    }
                },
            };
            this.rallies.Add(rally1);

            Rally rally2 = new Rally();
            //rally2.ID = this.rallies.Count;
            rally2.Name = "Schlo�berg Rally";
            rally2.Caption = "Ausblick inmitten der Altstadt";
            rally2.Description = new List<string>() {
                "IMG:./testpath/img.png",
                "TXT:Der Schlo�berg in Graz bildet mit 123 Metern H�he, ausgehend vom Grazer Hauptplatz, den h�chsten nat�rlichen Punkt der Stadt und bietet einen 360� Rundblick �ber die Stadt Graz und deren Grenzen hinaus. Beginnen wir mit der Geschichte des Schlo�bergs. Im 12. Jahrhundert wurde auf dem Schlo�berg eine Burg errichtet, die der Stadt Graz auch ihren Namen gab. Einer Ableitung aus �gradec� � dem slowenischen Begriff f�r kleine Burg. Da die Burg nie erobert wurde, ist sie im Guinness Buch der Rekorde als die st�rkste Festung aller Zeiten aufgelistet. Nicht einmal Napoleon schaffte es im 19. Jahrhundert die Burg einzunehmen. Erst als er durch die Besetzung Wiens 1809 Graz erpresste, Wien zu zerst�ren, ergab sich die Stadt Graz. Bis auf den Glockenturm und den Uhrturm, die von den Grazern freigekauft wurden, wurde die Burg im Gro�en und Ganzen abgetragen und gesprengt, eine sogenannte Schleifung. 30 Jahre sp�ter legte Ludwig Freiherr von Weldenman Spazierwege und einen romantischen Garten am Schlo�berg an." };
            rally2.Stations = new Dictionary<int, RallyStation>()
            {
                {
                    0,
                    new RallyStation()
                    {
                        Tasks= new List<RallyStationTask>()
                        {
                            new RallyStationTask()
                            {
                                TaskType = RallyStationTask.Type.GotoDestination,
                                Description = "Begib dich zum Kameliterplatz (Karmeliterpl., 8010 Graz)",
                                DestinationPoint = new Vector2(47.073880f, 15.440496f),
                            },
                            new RallyStationTask()
                            {
                                TaskType = RallyStationTask.Type.InfoScreen,
                                Description = "Hallo und herzlich willkommen bei der<br><b><size=130%>Schlo�berg-Townrallye!"
                            },
                            new RallyStationTask()
                            {
                                TaskType = RallyStationTask.Type.Game_Objectfind,
                                Description = "Um die Rallye zu starten, suche mit deinem Handy eine Teufelsfigur am Kamerliterplatz!",


                            },

                        },
                        //ID = 0,
                        //Name = "Schlo�berg Start",
                        //Description = "Die Mur entspringt in Salzburg, genauer gesagt in Muhr im Lungau. Die Mur durchflie�t drei L�nder: �sterreich, Slowenien, Ungarn und Kroatien, an manchen Stellen dient sie sogar als Landesgrenze. So erscheint es kaum verwunderlich, dass die Mur noch einen weiteren Namen tr�gt. Im Slowenischen, Kroatischen sowie Ungarischen wird sie Mura genannt. Mehrere Fl�sse verbinden sich mit der Mur, so m�ndet in Kroatien der Fluss Trnava in der n�rdlichen kroatischen Landschaft Me?imurje, was �bersetzt  auch �Zwischen der Mur� oder �Murinsel� bedeutet. Auch der gro�e Nebenfluss zur Donau, die Drau, m�ndet in die Mur. Die beiden gro�en Fl�sse treffen sich auch im n�rdlichen Kroatien, in der N�he der Ortschaft Legrad. Nach insgesamt 430km hat die Mur ihr Ziel erreicht: das Schwarze Meer. (Karte: Begriff Binnenmeer)\r\nDie Mur bildet den Hauptfluss der Steiermark, vor allem f�r die Gr�ndung von St�dten und D�rfern spielte sie in der Vergangenheit eine wichtige Rolle, schlie�lich bietet die N�he zu flie�enden Gew�ssern den Menschen viele Vorteile: frisches Trinkwasser, Fischfang und Handelswege w�ren drei davon. In der Steiermark tragen manche Ortschaften den Namen �Mur� sogar in ihrer Bezeichnung wie zum Beispiel Bruck an der Mur, Murau, und Mureck.",
                        
                    }
                },


                {
                    1,
                    new RallyStation()
                    {
                        Tasks= new List<RallyStationTask>()
                        {
                            new RallyStationTask()
                            {
                                TaskType = RallyStationTask.Type.InfoScreen,
                                Description = "Hallo und herzlich willkommen bei der Schlo�berg-Townrallye!<br><br><size=70%>Um die Rallye zu starten, �ffne bitte deine Kamera und suche damit am Kameliterplatz eine Teufelsfigur."
                            },
                            new RallyStationTask()
                            {
                                TaskType = RallyStationTask.Type.GotoDestination,
                                Description = "Hop hop, auf zum Uhrturm!"
                            },
                        },

                        //ID = 1,
                        //Name = "Ankunft an der Mur",
                        //Description = "Die Mur entspringt in Salzburg, genauer gesagt in Muhr im Lungau. Die Mur durchflie�t drei L�nder: �sterreich, Slowenien, Ungarn und Kroatien, an manchen Stellen dient sie sogar als Landesgrenze. So erscheint es kaum verwunderlich, dass die Mur noch einen weiteren Namen tr�gt. Im Slowenischen, Kroatischen sowie Ungarischen wird sie Mura genannt. Mehrere Fl�sse verbinden sich mit der Mur, so m�ndet in Kroatien der Fluss Trnava in der n�rdlichen kroatischen Landschaft Me?imurje, was �bersetzt  auch �Zwischen der Mur� oder �Murinsel� bedeutet. Auch der gro�e Nebenfluss zur Donau, die Drau, m�ndet in die Mur. Die beiden gro�en Fl�sse treffen sich auch im n�rdlichen Kroatien, in der N�he der Ortschaft Legrad. Nach insgesamt 430km hat die Mur ihr Ziel erreicht: das Schwarze Meer. (Karte: Begriff Binnenmeer)\r\nDie Mur bildet den Hauptfluss der Steiermark, vor allem f�r die Gr�ndung von St�dten und D�rfern spielte sie in der Vergangenheit eine wichtige Rolle, schlie�lich bietet die N�he zu flie�enden Gew�ssern den Menschen viele Vorteile: frisches Trinkwasser, Fischfang und Handelswege w�ren drei davon. In der Steiermark tragen manche Ortschaften den Namen �Mur� sogar in ihrer Bezeichnung wie zum Beispiel Bruck an der Mur, Murau, und Mureck.",
                    }
                }
            };
            this.rallies.Add(rally2);
        }

        private void CurrentTaskFinished()
        {
            Rally rally = Instance.rallies[Instance.currentRallyID];
            RallyStation rallyStation = rally.Stations[rally.currentStation];
            RallyStationTask task = rallyStation.Tasks[rallyStation.currentTask];
            if(rallyStation.currentTask+1 < rallyStation.Tasks.Count)
            {
                // display next task
                rallyStation.currentTask++;
            }
            else
            {
                if(rallyStation.NextPossibleStationIDs.Count > 0)
                {
                    // lead to next possible stations
                    // if they are not finished yet!
                    int notFinishedID = this.GetNotFinishedStationFromPool(rallyStation.NextPossibleStationIDs);
                    if(notFinishedID >= 0)
                    {
                        // there is one or more not finished stations from pool
                        // display goto_destination_screen
                    }
                    else
                    {
                        this.ContinueAtNextUnfinishedStation();
                    }
                }
                else
                {
                    this.ContinueAtNextUnfinishedStation();
                }
                
            }
            EventOut_RallyStationTaskChanged.Invoke();
        }

        private void ContinueAtNextUnfinishedStation()
        {
            Rally rally = Instance.rallies[Instance.currentRallyID];
            // all stations finished from pool -> goto next station in list
            int notFinishedID = this.GetNotFinishedStationFromPool(rally.Stations.Keys.ToList());
            if (notFinishedID >= 0)
            {
                // there is a not finished station -> do that.
            }
            else
            {
                // all stations are finished -> display end-screen
            }
        }

        private int GetNotFinishedStationFromPool(List<int> stations)
        {
            bool isAnyPossibleNotFinishedYet = false;
            Rally rally = Instance.rallies[Instance.currentRallyID];
            for (int i = 0; i < stations.Count; i++)
            {
                int stationID = stations[i];
                for (int j = 0; j < rally.Stations.Count; j++)
                {
                    if (rally.Stations.ContainsKey(stationID) && !rally.Stations[stationID].isFinished)
                    {
                        return stationID;
                    }
                }
            }
            return -1;
        }

        private void OnSetCurrentRally(int rallyID)
        {
            this.currentRallyID = rallyID;
            EventOut_RallyChanged.Invoke(this.rallies[this.currentRallyID]);
        }
    }
}
