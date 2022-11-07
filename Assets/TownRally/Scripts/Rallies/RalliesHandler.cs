
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
                "TXT:In der Stadt und auf der Höh’ – das schließt sich in Graz keineswegs aus. Der Fluss, die einzigartige Altstadt und mitten drin ein Berg. Der Grazer Schlossberg ist Sehenswürdigkeit, Naturschauspiel, Naherholungsgebiet und Aussichtspunkt zugleich. In kürzester Zeit gelangt man nach oben und genießt den herrlichen Ausblick auf Graz, die Altstadt und Umgebung. Einer Burg, die vor über 1.000 Jahren auf dem Schlossberg errichtet wurde, verdankt die Stadt ihren Namen. Aus dem slawischen Gradec für ,kleine Burg‘ wurde später Graz.\r\n\r\nSeit 1894 überwindet die Schlossbergbahn bravourös die rund 60% Steigung hinauf zum Schlossbergplateau - mit modernen Panoramagondeln.\r\n\r\nEine schnellere Art den Schlossberg zu erreichen, ist die Fahrt mit dem Schlossberglift.",
                "IMG:Rallies/SchlossbergRally/sbg_main.jpg",
                "TXT:In der Stadt und auf der Höh’ – das schließt sich in Graz keineswegs aus. Der Fluss, die einzigartige Altstadt und mitten drin ein Berg. Der Grazer Schlossberg ist Sehenswürdigkeit, Naturschauspiel, Naherholungsgebiet und Aussichtspunkt zugleich. In kürzester Zeit gelangt man nach oben und genießt den herrlichen Ausblick auf Graz, die Altstadt und Umgebung. Einer Burg, die vor über 1.000 Jahren auf dem Schlossberg errichtet wurde, verdankt die Stadt ihren Namen. Aus dem slawischen Gradec für ,kleine Burg‘ wurde später Graz.\r\n\r\nSeit 1894 überwindet die Schlossbergbahn bravourös die rund 60% Steigung hinauf zum Schlossbergplateau - mit modernen Panoramagondeln.\r\n\r\nEine schnellere Art den Schlossberg zu erreichen, ist die Fahrt mit dem Schlossberglift."};
            rally1.Stations = new Dictionary<int, RallyStation>()
            {
                {
                    0,
                    new RallyStation() {
                        //ID = 0,
                        //Name = "Ankunft an der Mur",
                        //Description = "Die Mur entspringt in Salzburg, genauer gesagt in Muhr im Lungau. Die Mur durchfließt drei Länder: Österreich, Slowenien, Ungarn und Kroatien, an manchen Stellen dient sie sogar als Landesgrenze. So erscheint es kaum verwunderlich, dass die Mur noch einen weiteren Namen trägt. Im Slowenischen, Kroatischen sowie Ungarischen wird sie Mura genannt. Mehrere Flüsse verbinden sich mit der Mur, so mündet in Kroatien der Fluss Trnava in der nördlichen kroatischen Landschaft Me?imurje, was übersetzt  auch „Zwischen der Mur“ oder „Murinsel“ bedeutet. Auch der große Nebenfluss zur Donau, die Drau, mündet in die Mur. Die beiden großen Flüsse treffen sich auch im nördlichen Kroatien, in der Nähe der Ortschaft Legrad. Nach insgesamt 430km hat die Mur ihr Ziel erreicht: das Schwarze Meer. (Karte: Begriff Binnenmeer)\r\nDie Mur bildet den Hauptfluss der Steiermark, vor allem für die Gründung von Städten und Dörfern spielte sie in der Vergangenheit eine wichtige Rolle, schließlich bietet die Nähe zu fließenden Gewässern den Menschen viele Vorteile: frisches Trinkwasser, Fischfang und Handelswege wären drei davon. In der Steiermark tragen manche Ortschaften den Namen „Mur“ sogar in ihrer Bezeichnung wie zum Beispiel Bruck an der Mur, Murau, und Mureck.",
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
                        //Description = "Wer sagt, dass Graz keine Insel besitzt, der war wohl noch nie auf der berühmten Murinsel! \r\nSeit dem Jahr 2003, als Graz Kulturhauptstadt Europas war, gibt es die schwimmende Insel auf der Mur. \r\nDie Idee für Die Insel in der Mur stammt von dem gebürtigen Grazer Robert Punkenhofer und seiner Firma Art & Idea. Umgesetzt wurde das Projekt Murinsel von Vito Acconci, einem New Yorker Künstler. \r\nOptisch erinnert die Form der Murinsel an eine Muschel. Jede Muschelfläche erfüllt dabei eine eigene Funktion, wie das Amphitheater, das Cafe, oder Spielplatz am Dach. \r\nDer Künstler Acconci selbst beschreibt die Murinsel mit den Worten: „…das Wasser rund um diese Insel fließt und bewegt sich ständig, und wir wollten etwas konstruieren, das ebenfalls flüssig und veränderlich ist.\"\r\nBetrachtet man die Materialien genauer fällt auf, dass die Grazer Hügellandschaft mit den zu Hügeln geformten Edelstahlelementen dargestellt wurde. Die Konstruktion der Insel selbst besteht aus einem Stahlskelett, auch die Stege, die die Insel mit den Ufern verbinden, bestehen aus Stahl. Der asphaltierte Gehweg ermöglicht durch eine Beheizung auch im Winter eine rutschfreie Begehung. \r\nDie Mur trennt Graz in zwei Teile und die Murinsel stellt seit 2003 eine wichtige Verbindung dieser beiden Stadtteile dar. Ihre Stege verbinden den Mariahilferplatz mit dem Schloßbergplatz. \r\nDie Grazer Murinsel ist 50 Meter lang und 20 Meter breit, mit ihren 450 Tonnen ist sie zwar nicht gerade leicht, jedoch hält sie auch stürmischen Regenfällen und damit verbunden hohem Wasserstand auf der Mur stand. Die Kosten für dieses Bauprojekt betrugen ca. 5,75 Millionen €. \r\n",
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
                        //Description = "Runde Steine mit bewegter Geschichte\r\nMurnockerl sind Dokumente der letzten Eiszeit in unseren Alpen und bilden über weite Flächen den geologischen Untergrund der Stadt Graz und der südlich anschließenden Verebnungen. Täglich marschieren wir also über tausende von Murnockerln. Die runden Steine sind ein bedeutender Grundwasserkörper im Steirischen Becken und Massenrohstoffe. Jeder Österreicher verbraucht im Durchschnitt 12 Tonnen mineralische Rohstoffe pro Jahr, ein Großteil davon wird verbaut.\r\nFarbe, Form und Größe der Murnockerln erzählen uns von ihrer Zusammensetzung, ihrem Herkunftsort und ihrem meist turbulenten Transportweg im Wasser. Ein Gesteinsanschliff zeigt uns Strukturen und den Internaufbau der gerundeten Gesteine. Bei Betrachtung eines Gesteinsdünnschliffes im Mikroskop sehen wir die farbenprächtige mineralische Zusammensetzung der „grauen Steine\".\r\n\r\nGesteine sind das Archiv unserer Erde, sie erzählen (Erd-)Geschichten: über die Veränderung der Landschaft, des Klimas, des Lebens auf unserer Erde, letztlich: auch über uns selbst. In der Langen Nacht der Forschung informieren wir über die Bedeutung und den Informationsgehalt der runden Steine und Sie haben die Möglichkeit, mittels eines Polarisationsmikroskops durch ein Gestein zu schauen.https://www.museum-joanneum.at/naturkundemuseum/ihr-besuch/programm/goe-paleo/events/event/2808/was-sind-eigentlich-murnockerl \r\nDie Maria Theresien Allee als Verkehrsweg lässt sich bis ins Mittelalter zurückverfolgen. Vom Paulustor zum Geidorfplatz wird diese Allee heute auf fast der gesamten Länge von einem Gehweg mit einer Mosaiksteinoberfläche aus kleinen Flusssteinen, den sog. \"Murnockerln\", wie von einem ausgerollten Teppich begleitet. Dieses Grazer Kieselornamentband ist einzigartig in Graz und Österreich.\r\n\r\n",
                    }
                },
            };
            this.rallies.Add(rally1);

            Rally rally2 = new Rally();
            //rally2.ID = this.rallies.Count;
            rally2.Name = "Schloßberg Rally";
            rally2.Caption = "Ausblick inmitten der Altstadt";
            rally2.Description = new List<string>() {
                "IMG:./testpath/img.png",
                "TXT:Der Schloßberg in Graz bildet mit 123 Metern Höhe, ausgehend vom Grazer Hauptplatz, den höchsten natürlichen Punkt der Stadt und bietet einen 360° Rundblick über die Stadt Graz und deren Grenzen hinaus. Beginnen wir mit der Geschichte des Schloßbergs. Im 12. Jahrhundert wurde auf dem Schloßberg eine Burg errichtet, die der Stadt Graz auch ihren Namen gab. Einer Ableitung aus „gradec“ – dem slowenischen Begriff für kleine Burg. Da die Burg nie erobert wurde, ist sie im Guinness Buch der Rekorde als die stärkste Festung aller Zeiten aufgelistet. Nicht einmal Napoleon schaffte es im 19. Jahrhundert die Burg einzunehmen. Erst als er durch die Besetzung Wiens 1809 Graz erpresste, Wien zu zerstören, ergab sich die Stadt Graz. Bis auf den Glockenturm und den Uhrturm, die von den Grazern freigekauft wurden, wurde die Burg im Großen und Ganzen abgetragen und gesprengt, eine sogenannte Schleifung. 30 Jahre später legte Ludwig Freiherr von Weldenman Spazierwege und einen romantischen Garten am Schloßberg an." };
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
                                Description = "Hallo und herzlich willkommen bei der<br><b><size=130%>Schloßberg-Townrallye!"
                            },
                            new RallyStationTask()
                            {
                                TaskType = RallyStationTask.Type.Game_Objectfind,
                                Description = "Um die Rallye zu starten, suche mit deinem Handy eine Teufelsfigur am Kamerliterplatz!",


                            },

                        },
                        //ID = 0,
                        //Name = "Schloßberg Start",
                        //Description = "Die Mur entspringt in Salzburg, genauer gesagt in Muhr im Lungau. Die Mur durchfließt drei Länder: Österreich, Slowenien, Ungarn und Kroatien, an manchen Stellen dient sie sogar als Landesgrenze. So erscheint es kaum verwunderlich, dass die Mur noch einen weiteren Namen trägt. Im Slowenischen, Kroatischen sowie Ungarischen wird sie Mura genannt. Mehrere Flüsse verbinden sich mit der Mur, so mündet in Kroatien der Fluss Trnava in der nördlichen kroatischen Landschaft Me?imurje, was übersetzt  auch „Zwischen der Mur“ oder „Murinsel“ bedeutet. Auch der große Nebenfluss zur Donau, die Drau, mündet in die Mur. Die beiden großen Flüsse treffen sich auch im nördlichen Kroatien, in der Nähe der Ortschaft Legrad. Nach insgesamt 430km hat die Mur ihr Ziel erreicht: das Schwarze Meer. (Karte: Begriff Binnenmeer)\r\nDie Mur bildet den Hauptfluss der Steiermark, vor allem für die Gründung von Städten und Dörfern spielte sie in der Vergangenheit eine wichtige Rolle, schließlich bietet die Nähe zu fließenden Gewässern den Menschen viele Vorteile: frisches Trinkwasser, Fischfang und Handelswege wären drei davon. In der Steiermark tragen manche Ortschaften den Namen „Mur“ sogar in ihrer Bezeichnung wie zum Beispiel Bruck an der Mur, Murau, und Mureck.",
                        
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
                                Description = "Hallo und herzlich willkommen bei der Schloßberg-Townrallye!<br><br><size=70%>Um die Rallye zu starten, öffne bitte deine Kamera und suche damit am Kameliterplatz eine Teufelsfigur."
                            },
                            new RallyStationTask()
                            {
                                TaskType = RallyStationTask.Type.GotoDestination,
                                Description = "Hop hop, auf zum Uhrturm!"
                            },
                        },

                        //ID = 1,
                        //Name = "Ankunft an der Mur",
                        //Description = "Die Mur entspringt in Salzburg, genauer gesagt in Muhr im Lungau. Die Mur durchfließt drei Länder: Österreich, Slowenien, Ungarn und Kroatien, an manchen Stellen dient sie sogar als Landesgrenze. So erscheint es kaum verwunderlich, dass die Mur noch einen weiteren Namen trägt. Im Slowenischen, Kroatischen sowie Ungarischen wird sie Mura genannt. Mehrere Flüsse verbinden sich mit der Mur, so mündet in Kroatien der Fluss Trnava in der nördlichen kroatischen Landschaft Me?imurje, was übersetzt  auch „Zwischen der Mur“ oder „Murinsel“ bedeutet. Auch der große Nebenfluss zur Donau, die Drau, mündet in die Mur. Die beiden großen Flüsse treffen sich auch im nördlichen Kroatien, in der Nähe der Ortschaft Legrad. Nach insgesamt 430km hat die Mur ihr Ziel erreicht: das Schwarze Meer. (Karte: Begriff Binnenmeer)\r\nDie Mur bildet den Hauptfluss der Steiermark, vor allem für die Gründung von Städten und Dörfern spielte sie in der Vergangenheit eine wichtige Rolle, schließlich bietet die Nähe zu fließenden Gewässern den Menschen viele Vorteile: frisches Trinkwasser, Fischfang und Handelswege wären drei davon. In der Steiermark tragen manche Ortschaften den Namen „Mur“ sogar in ihrer Bezeichnung wie zum Beispiel Bruck an der Mur, Murau, und Mureck.",
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
