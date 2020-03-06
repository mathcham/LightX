﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using LightX_01.Classes;
using System;

namespace JsonFileWriter
{
    class Program
    {
        static void Main()
        {
            List<GuideData> jsonData = new List<GuideData>();

            GuideData Conjonctive = new GuideData()
            {
                FileName = "Conjonctive.json",
                TestTitle = "Conjonctive",
                SlitIntensity = "0",
                IllumAngle = "-",
                DiffuseIntensity = "8",
                CamSettings = new CameraSettings() { Flash = "ON", ShutterSpeed = "1/30", FNumber = "5.6", Iso = "200", BurstNumber = "2"},
                InstructionsNotes = "Ayy lmao",
                ImagesPath = new List<string>() { @"Resources\01_01.png", @"\\Resources\02_01.png" }

            };

            GuideData VanHerick = new GuideData()
            {
                FileName = "VanHerick.json",
                TestTitle = "Van Herick",
                SlitIntensity = "10",
                IllumAngle = "60°",
                DiffuseIntensity = "2",
                CamSettings = new CameraSettings() { Flash = "OFF", ShutterSpeed = "1/60", FNumber = "11.0", Iso = "400", BurstNumber = "1"},
                InstructionsNotes = "On désir estimer l'espace entre la cornée et l'iris en terme d'épaisseur de cornée.",
                ImagesPath = new List<string>() { @"Resources\02_01.png" }
            };

            GuideData Cornea = new GuideData()
            {
                FileName = "Cornea.json",
                TestTitle = "Conée",
                SlitIntensity = "10",
                IllumAngle = "45°",
                DiffuseIntensity = "2",
                CamSettings = new CameraSettings() { Flash = "OFF", ShutterSpeed = "1/30", FNumber = "11.0", Iso = "400", BurstNumber = "2"},
                InstructionsNotes = "On désir observer toutes anomalies, défauts, ammincicement en surface/à l'intérieur/derrière la cornée.",
                ImagesPath = new List<string>() { @"Resources\01_01.png" }
            };

            GuideData Anterior = new GuideData()
            {
                FileName = "AnteriorChamber.json",
                TestTitle = "Chambre Antérieure",
                SlitIntensity = "10",
                IllumAngle = "40°",
                DiffuseIntensity = "2",
                CamSettings = new CameraSettings() { Flash = "ON", ShutterSpeed = "1/30", FNumber = "5.6", Iso = "200", BurstNumber = "1" },
                InstructionsNotes = "On désir observer s'il y a présence de cellules et/ou de flares.  Désire que la prise de vue se fasse devant l'entrée de la pupille pour obtenir un arrière plan noir (meilleurs contraste).",
                ImagesPath = new List<string>() { @"Resources\02_01.png" }
            };

            GuideData Lens = new GuideData()
            {
                FileName = "Lens.json",
                TestTitle = "Cristallin",
                SlitIntensity = "10",
                IllumAngle = "40°",
                DiffuseIntensity = "2",
                CamSettings = new CameraSettings() { Flash = "ON", ShutterSpeed = "1/30", FNumber = "5.6", Iso = "200", BurstNumber = "1" },
                InstructionsNotes = "On désir observer s'il y a présence d'opacité ou changement de couleur dans le cristallin.",
                ImagesPath = new List<string>() { @"Resources\02_01.png" }
            };

            GuideData PupillaryMargin = new GuideData()
            {
                FileName = "PupillaryMargin.json",
                TestTitle = "Marges Pupillaires",
                SlitIntensity = "0",
                IllumAngle = "40°",
                DiffuseIntensity = "2",
                CamSettings = new CameraSettings() { Flash = "ON", ShutterSpeed = "1/30", FNumber = "5.6", Iso = "200", BurstNumber = "1" },
                InstructionsNotes = "On désir observer s'il y a présence d'irrégularité ou de dépôt sur la marge.",
                ImagesPath = new List<string>() { @"Resources\02_01.png" }
            };

            GuideData IrisTransillumination = new GuideData()
            {
                FileName = "IrisTransillumination.json",
                TestTitle = "Transillumination de l'iris",
                SlitIntensity = "10",
                IllumAngle = "10°",
                DiffuseIntensity = "0",
                CamSettings = new CameraSettings() { Flash = "ON", ShutterSpeed = "1/30", FNumber = "5.6", Iso = "200", BurstNumber = "1" },
                InstructionsNotes = "On désir observer s'il y a présence d'irrégularité ou de dépôt sur la marge.",
                ImagesPath = new List<string>() { @"Resources\02_01.png" }
            };

            GuideData Cobalt = new GuideData()
            {
                FileName = "CobaltFilter.json",
                TestTitle = "Filtre Cobalt",
                SlitIntensity = "5",
                IllumAngle = "30°",
                DiffuseIntensity = "0",
                CamSettings = new CameraSettings() { Flash = "ON", ShutterSpeed = "1/30", FNumber = "5.6", Iso = "200", BurstNumber = "1" },
                InstructionsNotes = "Administer les gouttes de fluorescéine au patient et laissez agir. On désir observer s'il y a présence d'abrasion/défaut dans la cornée.",
                ImagesPath = new List<string>() { @"Resources\02_01.png" }
            };


            jsonData.Add(Conjonctive);
            jsonData.Add(VanHerick);
            jsonData.Add(Cornea);
            jsonData.Add(Anterior);
            jsonData.Add(Lens);
            jsonData.Add(PupillaryMargin);
            jsonData.Add(IrisTransillumination);
            jsonData.Add(Cobalt);

            WriteJsonFiles(@"..\..\..\LightX_01\Resources\", jsonData);
            WriteJsonFiles(@"..\..\..\LightX_01\bin\Debug\Resources\", jsonData);

        }
        static void WriteJsonFiles(string path, List<GuideData> jsonData)
        {
            foreach (GuideData data in jsonData)
            {
                using (StreamWriter file = File.CreateText($"{path}{data.FileName}"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, data);
                }
            }

        }

    }
}