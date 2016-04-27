﻿using System.Collections.Generic;
using System.Linq;

using TransformerAssessment.Core.Loaders;

namespace TransformerAssessment.Core.Objects {
    class Transformer {
        private string equipID;
        public string serialNum;
        public string norm;
        public string manufacturer;
        public string yearMade;
        public string oilPresType;
        public string remarks;
        private string substn_name;
        private string designation;
        public string[] rawXFMR;

        public string[] equipmentHeaders = null;
        public List<TestData> data = new List<TestData>();
        public TapChanger ltc;
        public Selector sel;
        public Diverter div;

        public bool hasLTC;
        public bool hasSEL;
        public bool hasDIV;

        public Transformer(string[] transformer) {
            equipID = transformer[EquipmentLoader.equipnumIndex];
            serialNum = transformer[EquipmentLoader.serialnumIndex];
            norm = transformer[EquipmentLoader.norm_nameIndex];
            manufacturer = transformer[EquipmentLoader.mfrIndex];
            yearMade = transformer[EquipmentLoader.year_mfgIndex];
            oilPresType = transformer[EquipmentLoader.oilpresIndex];
            remarks = transformer[EquipmentLoader.eqp_remarksIndex];
            substn_name = transformer[EquipmentLoader.substn_nameIndex];
            designation = transformer[EquipmentLoader.designationIndex];

            equipmentHeaders = EquipmentLoader.headers;
        }

        public Transformer(string[] transformer, string[] ltc, string[] sel, string[] div) {
            equipID = transformer[EquipmentLoader.equipnumIndex];
            serialNum = transformer[EquipmentLoader.serialnumIndex];
            norm = transformer[EquipmentLoader.norm_nameIndex];
            manufacturer = transformer[EquipmentLoader.mfrIndex];
            yearMade = transformer[EquipmentLoader.year_mfgIndex];
            oilPresType = transformer[EquipmentLoader.oilpresIndex];
            remarks = transformer[EquipmentLoader.eqp_remarksIndex];
            substn_name = transformer[EquipmentLoader.substn_nameIndex];
            designation = transformer[EquipmentLoader.designationIndex];

            equipmentHeaders = EquipmentLoader.headers;

            rawXFMR = transformer;
            //Console.WriteLine("Creating transformer: {0}, {1} {2}", equipID, substn_name, designation);
            if (ltc.Length > 0)
                addLTC(ltc);
            if (sel.Length > 0)
                addSEL(sel);
            if (div.Length > 0)
                addDIV(div);
        }

        /// <summary>Adds data[] to Tranformer instance.
        /// <param name="data">string[] from 'data.csv' row</param>
        /// </summary>
        public void addData(string[] data) {
            //Console.WriteLine("Adding data to {0} {1} {2}", substn_name, designation, data[TOAExportLoader.data_apprtypeIndex]);
            // if data is LTC, add to ltc
            if (hasLTC && data[TestDataLoader.apprtypeIndex].Equals("LTC"))
                ltc.addData(data);
            // if data is SEL, add to sel
            else if (hasSEL && data[TestDataLoader.apprtypeIndex].Equals("SEL"))
                sel.addData(data);
            // if data is DIV, add to div
            else if (hasDIV && data[TestDataLoader.apprtypeIndex].Equals("DIV"))
                div.addData(data);
            else if (data[TestDataLoader.apprtypeIndex].Equals("XFMR"))
                addXFMRData(data);
        }

        private void addXFMRData(string[] data) {
            this.data.Add(new TestData(data));
            this.data = this.data.OrderByDescending(m => m._date).ToList();
        }

        public void addLTC(string[] equipment) {
            ltc = new TapChanger(equipment);
            hasLTC = true;
        }

        public void addSEL(string[] equipment) {
            sel = new Selector(equipment);
            hasSEL = true;
        }

        public void addDIV(string[] equipment) {
            div = new Diverter(equipment);
            hasDIV = true;
        }

        #region [Methods] Getters
        public string getID() { return equipID; }
        public string getLocation() { return substn_name; }
        public string getPosition() { return designation; }
        #endregion
    }
}