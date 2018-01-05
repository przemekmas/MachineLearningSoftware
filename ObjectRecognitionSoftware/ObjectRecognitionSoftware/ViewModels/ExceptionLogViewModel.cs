﻿using ObjectRecognitionSoftware.Common;
using ObjectRecognitionSoftware.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;

namespace ObjectRecognitionSoftware.ViewModels
{
    public class ExceptionLogViewModel : NotifyPropertyChanged
    {
        private List<ExceptionEntity> m_ExceptionList;
        private string m_ExceptionCount;
        private string m_ExceptionDetails;

        public List<ExceptionEntity> ExceptionList
        {
            get { return m_ExceptionList; }
            set
            {
                m_ExceptionList = value;
                OnPropertyChanged(nameof(ExceptionList));
            }
        }

        public string ExceptionCount
        {
            get { return m_ExceptionCount; }
            set
            {
                m_ExceptionCount = value;
                OnPropertyChanged(nameof(ExceptionCount));
            }
        }

        public string ExceptionDetails
        {
            get { return m_ExceptionDetails; }
            set
            {
                m_ExceptionDetails = value;
                OnPropertyChanged(nameof(ExceptionDetails));
            }
        }
        
        public ExceptionLogViewModel()
        {
            ExceptionList = ExceptionLogging.GetExceptions();
            UpdateExceptionCount(ExceptionList.Count);
            InitiateTimer();
        }
                
        public void InitiateTimer()
        {
            var timerEvent = new TimerEvent();
            timerEvent.InititateTimer(1000, OnUpdateEvent);
        }

        public void SetExceptionDetails(string exception)
        {
            ExceptionDetails = exception;
        }

        private void OnUpdateEvent(Object source, ElapsedEventArgs e)
        {
            if(ExceptionList.Count < ExceptionLogging.GetExceptions().Count)
            {
                ExceptionList = ExceptionLogging.GetExceptions();
                UpdateExceptionCount(ExceptionList.Count);
            }            
        }

        private void UpdateExceptionCount(int exceptionValue)
        {
            ExceptionCount = string.Format(Properties.ExceptionLogResource.ExceptionCount, exceptionValue);
        }
    }
}