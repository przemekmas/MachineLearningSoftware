using ObjectRecognitionSoftware.Common;
using ObjectRecognitionSoftware.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;

namespace ObjectRecognitionSoftware.ViewModels
{
    public class ExceptionLogViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private List<ExceptionEntity> m_ExceptionList;

        public List<ExceptionEntity> ExceptionList
        {
            get { return m_ExceptionList; }
            set
            {
                m_ExceptionList = value;
                OnPropertyChanged(nameof(ExceptionList));
            }
        }
        
        public ExceptionLogViewModel()
        {
            ExceptionList = ExceptionLogging.GetExceptions();
            InitiateTimer();
        }
        
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        
        public void InitiateTimer()
        {
            var timerEvent = new TimerEvent();
            timerEvent.InititateTimer(1000, OnUpdateEvent);
        }

        private void OnUpdateEvent(Object source, ElapsedEventArgs e)
        {
            if(ExceptionList.Count < ExceptionLogging.GetExceptions().Count)
            {
                ExceptionList = ExceptionLogging.GetExceptions();
            }            
        }
    }
}
