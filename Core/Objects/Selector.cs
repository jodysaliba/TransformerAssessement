﻿using System;
using System.Collections.Generic;
using System.Linq;
using TransformerAssessment.Core.Loaders;

namespace TransformerAssessment.Core.Objects {
    class Selector {
        public List<TestData> data = new List<TestData>();
        public string norm;
        public string manufacturer;
        public string yearMade;
        public string oilPresType;
        public string remarks;
        public string model;

        /// <summary>
        /// Calculated health of the SEL object
        /// </summary>
        public double Health { get; set; }

        public Selector(string[] sel) {
            norm = sel[EquipmentLoader.norm_nameIndex];
            manufacturer = sel[EquipmentLoader.mfrIndex];
            yearMade = sel[EquipmentLoader.year_mfgIndex];
            oilPresType = sel[EquipmentLoader.oilpresIndex];
            remarks = sel[EquipmentLoader.eqp_remarksIndex];
            model = sel[EquipmentLoader.modelIndex];
        }

        public void addData(string[] data) {
            this.data.Add(new TestData(data));
            this.data = this.data.OrderByDescending(m => m._date).ToList();
        }
    }
}
