﻿using System;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;

namespace WachtrijApp
{
    public partial class FormWachtrij : Form
    {
        public FormWachtrij()
        {
            InitializeComponent();
        }

        int BTN_Clicks = 1;

        //  Er is op het logo geklikt. Die actie start een event welke de onderstaande methode aanroept.
        private void AttractieLogo_Click(object sender, EventArgs e)
        {
            VerwerkWachtrijSensorData();

            VerwerkAttractieStatusData();

            EA_ResetTime.Enabled = true;
            BTN_Clicks = BTN_Clicks + 1;
        }

        private void EA_ResetTime_Tick(object sender, EventArgs e)
        {
            EA_ResetTime.Enabled = false;
            if (BTN_Clicks == 3){
                MoveImg.Enabled = true;
                labelKar1.Visible = false;
                labelKar2.Visible = false;
                labelKar3.Visible = false;
                labelTitel.Visible = false;
                labelWachttijd.Visible = false;
                labelWachttijdMelding.Visible = false;
                btnReset.Visible = false;
            }
            BTN_Clicks = 1;
        }

        private void VerwerkWachtrijSensorData()
        {
            //  Roep de methode aan welke de wachttijd berekend.
            double Wachttijd = BerekenWachttijd();

            //  Gebruik de wachttijd om de tekst in de label 'labelWachttijdMelding' aan te passen.
            this.labelWachttijdMelding.Text = $"{Wachttijd} minuten";
        }

        //  Deze methode leest de sensordata in het betreffende XML bestand.
        //  Deze methode berekend vervolgens de de wachttijd in minuten en geeft deze terug.
        private double BerekenWachttijd()
        {
            
            double Wachttijd = 0;
           


            //  Lees het XML WachtrijSensoren bestand uit welke meet waar mensen staan te wachten.
            XmlDocument doc = new XmlDocument();
            doc.Load("SensorData\\WachtrijSensoren.xml");

            //  Selecteer de XML node 'Sensor01' en lees vervolgens de waarde binnen het element.
            //  Wanneer de sensor geen mensen detecteerd, geef de tot nu to berekende wachttijd terug.
            //  Wanneer de sensor wel mensen detecteerd, bereken de nieuwe wachttijd en ga door naar de volgende sensor.
            string node01 = doc.DocumentElement.SelectSingleNode("/Sensoren/Sensor01").InnerText;
            if (node01 == "False")
            {
                return Wachttijd;
            }
            Wachttijd += 6;

            string node02 = doc.DocumentElement.SelectSingleNode("/Sensoren/Sensor02").InnerText;
            if (node02 == "False")
            {
                return Wachttijd;
            }
            Wachttijd += 6;

            string node03 = doc.DocumentElement.SelectSingleNode("/Sensoren/Sensor03").InnerText;
            if (node03 == "False")
            {
                return Wachttijd;
            }
            Wachttijd += 3.5;

            string node04 = doc.DocumentElement.SelectSingleNode("/Sensoren/Sensor04").InnerText;
            if (node04 == "False")
            {
                return Wachttijd;
            }
            Wachttijd += 3.5;

            string node05 = doc.DocumentElement.SelectSingleNode("/Sensoren/Sensor05").InnerText;
            if (node05 == "False")
            {
                return Wachttijd;
            }
            Wachttijd += 3.5;

            string node06 = doc.DocumentElement.SelectSingleNode("/Sensoren/Sensor06").InnerText;
            if (node06 == "False")
            {
                return Wachttijd;
            }
            Wachttijd += 3.5;

            string node07 = doc.DocumentElement.SelectSingleNode("/Sensoren/Sensor07").InnerText;
            if (node07 == "False")
            {
                return Wachttijd;
            }
            Wachttijd += 3.5;

            string node08 = doc.DocumentElement.SelectSingleNode("/Sensoren/Sensor08").InnerText;
            if (node08 == "False")
            {
                return Wachttijd;
            }
            Wachttijd += 3.5;

            string node09 = doc.DocumentElement.SelectSingleNode("/Sensoren/Sensor09").InnerText;
            if (node08 == "False")
            {
                return Wachttijd;
            }
            Wachttijd += 3.5;

            string node10 = doc.DocumentElement.SelectSingleNode("/Sensoren/Sensor10").InnerText;
            if (node10 == "False")
            {
                return Wachttijd;
            }
            Wachttijd += 3.5;

            return Wachttijd;
        }

        private void VerwerkAttractieStatusData()
        {
            //  Lees het XML AttractieStatus bestand uit welke de data van de karretjes uitleest.
            XmlDocument doc = new XmlDocument();
            doc.Load("SensorData\\AttractieStatus.xml");

            //  Selecteer de XML node 'Kar01' en lees vervolgens de waarde binnen het element.
            //  Converteer de statuc-code in een status-beschrijving.
            //  Gebruik de status-beschrijving om de tekst in de label 'labelKar1' aan te passen.
            string node1 = doc.DocumentElement.SelectSingleNode("/Status/Kar01").InnerText;
            string status1 = ConvertStatus(node1);
            this.labelKar1.Text = $"Kar 1: {status1}";

            string node2 = doc.DocumentElement.SelectSingleNode("/Status/Kar02").InnerText;
            string status2 = ConvertStatus(node2);
            this.labelKar2.Text = $"Kar 2: {status2}";

            string node3 = doc.DocumentElement.SelectSingleNode("/Status/Kar03").InnerText;
            string status3 = ConvertStatus(node3);
            this.labelKar3.Text = $"Kar 3: {status3}";
        }

        //  Een methode welke een status-code omzet naar een status-beschrijving
        private string ConvertStatus(string StatusNr)
        {
            if (StatusNr == "1")
            {
                return "uit/instappen";
            }

            if (StatusNr == "2")
            {
                return "Klaar voor vertrek";
            }

            if (StatusNr == "3")
            {
                return "Op avontuur";
            }

            if (StatusNr == "4")
            {
                return "Komt binnen";
            }
            if (StatusNr =="5")
            {
                return "in onderhoud";
            }

            return "";
        }

        private void MoveImg_Tick(object sender, EventArgs e)
        {
            AttractieLogo.Left = AttractieLogo.Left - 5;
            AttractieLogo.Top = AttractieLogo.Top - 2;
            if (AttractieLogo.Left < -200 && AttractieLogo.Top < -100) {
                MoveImg.Enabled = false;
                AttractieLogo.Left = 276;
                AttractieLogo.Top = 121;

                labelKar1.Visible = true;
                labelKar2.Visible = true;
                labelKar3.Visible = true;
                labelTitel.Visible = true;
                labelWachttijd.Visible = true;
                labelWachttijdMelding.Visible = true;
                btnReset.Visible = true;
            }
        }
        
        private void btnReset_Click(object sender, EventArgs e)
        {
            VerwerkAttractieStatusData();
        }
    }
}


