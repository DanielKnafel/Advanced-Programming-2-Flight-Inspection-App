﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Ex1.controls.GraphReg
{
    class GraphRegViewModel : ViewModel
    {

        public string VM_DisplayFeature { get { return vm.DisplayFeature; } }
        public string VM_CorrolatedFeature { get { return vm.getCorrelatedFeature(VM_DisplayFeature); } }

        public List<float> getValuesOfFeature(string name)
        {
            return vm.getValuesOfFeature(name);
        }

        public GraphRegViewModel() { }

        public List<int> CurrentAnomalies
        { 
            get
            { //return all anomalies of current feature.
                List<int> currentAnomalies = new List<int>();
                string features1 = VM_DisplayFeature + "-" + VM_CorrolatedFeature;
                string features2 = VM_CorrolatedFeature + "-" + VM_DisplayFeature;

                foreach (Tuple<string, int> t in vm.Anomalies)
                {
                    if (features1.Equals(t.Item1) || features2.Equals(t.Item1))
                        currentAnomalies.Add(t.Item2);
                }
                return currentAnomalies;
            }
        }

        public int LineNumber
        {
            get { return vm.LineNumber; }
            set { vm.LineNumber = value; }
        }
        public override void setMainViewModel(MainViewModel vm)
        {
            base.setMainViewModel(vm);
            this.vm.PropertyChanged += //listen to MainViewModel.
               delegate (Object sender, PropertyChangedEventArgs e)
               {
                   if (e.PropertyName.Equals("DisplayFeature"))
                   {
                       NotifyPropertyChanged("VM_CorrolatedFeature");
                   }
                   NotifyPropertyChanged("VM_" + e.PropertyName);
               };
        }
        public Line RegLine
        {
            get { return vm.RegLine; }
        }
        public List<Point> getPoints()
        {
            List<Point> list = new List<Point>();
            if (VM_CorrolatedFeature != null) //get all points of feature.
            {
                List<float> val1 = vm.getValuesOfFeature(VM_CorrolatedFeature);
                List<float> val2 = vm.getValuesOfFeature(VM_DisplayFeature);
                for (int i = 0; i < vm.Size; i++)
                {
                    list.Add(new Point(val1[i], val2[i]));
                }
                return list;
            }
            return list;
        }
        public float DisplayFeatureMinValue
        {
            get { return vm.DisplayFeatureMinValue; }
        }
        public float CorrolateFeatureMinValue
        {
            get { return vm.CorrolateFeatureMinValue; }
        }
        public float DisplayFeatureMaxValue
        {
            get { return vm.DisplayFeatureMaxValue; }
        }
        public float CorrolateFeatureMaxValue
        {
            get { return vm.CorrolateFeatureMaxValue; }
        }
        public int Size { get { return vm.Size; } }
        public int Frequency { get { return vm.Frequency; } }
    }
}
