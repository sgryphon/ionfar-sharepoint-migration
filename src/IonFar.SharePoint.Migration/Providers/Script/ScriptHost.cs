﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Management.Automation.Host;
using System.Threading;

namespace IonFar.SharePoint.Migration.Providers.Script
{
    public class ScriptHost : PSHost
    {
        private int _exitCode;
        private readonly Guid _id;
        private readonly CultureInfo _originalCultureInfo;
        private readonly CultureInfo _originalUICultureInfo;
        private readonly PSHostUserInterface _hostUI;
        private readonly Version _version;
        
        public ScriptHost(IUpgradeLog logger)
        {
            _hostUI = new ScriptHostUI(logger);
            _id = Guid.NewGuid();
            _originalCultureInfo = Thread.CurrentThread.CurrentCulture;
            _originalUICultureInfo = Thread.CurrentThread.CurrentUICulture;

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            _version = new Version(fvi.FileMajorPart, fvi.FileMinorPart, fvi.FileBuildPart, fvi.FilePrivatePart);
        }

        /// <summary>
        /// Return the culture information to use. This implementation 
        /// returns a snapshot of the culture information of the thread 
        /// that created this object.
        /// </summary>
        public override CultureInfo CurrentCulture
        {
            get { return _originalCultureInfo; }
        }

        /// <summary>
        /// Return the UI culture information to use. This implementation 
        /// returns a snapshot of the UI culture information of the thread 
        /// that created this object.
        /// </summary>
        public override CultureInfo CurrentUICulture
        {
            get { return _originalUICultureInfo; }
        }

        public int ExitCode
        {
            get { return _exitCode; }
        }

        /// <summary>
        /// This implementation always returns the GUID allocated at 
        /// instantiation time.
        /// </summary>
        public override Guid InstanceId
        {
            get { return _id; }
        }

        public override string Name
        {
            get { return "IonFar SharePoint Migration Host"; }
        }

        public int ShouldExit
        {
            get { return _exitCode; }
        }

        public override PSHostUserInterface UI
        {
            get { return _hostUI; }
        }

        public override Version Version
        {
            get { return _version; }
        }


        public override void EnterNestedPrompt()
        {
            throw new NotImplementedException();
        }

        public override void ExitNestedPrompt()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This API is called before an external application process is 
        /// started. Typically it is used to save state so the parent can 
        /// restore state that has been modified by a child process (after 
        /// the child exits). In this example, this functionality is not  
        /// needed so the method returns nothing.
        /// </summary>
        public override void NotifyBeginApplication()
        {
        }

        /// <summary>
        /// This API is called after an external application process finishes.
        /// Typically it is used to restore state that a child process may
        /// have altered. In this example, this functionality is not  
        /// needed so the method returns nothing.
        /// </summary>
        public override void NotifyEndApplication()
        {
        }

        /// <summary>
        /// Indicate to the host application that exit has
        /// been requested. Pass the exit code that the host
        /// application should use when exiting the process.
        /// </summary>
        /// <param name="exitCode">The exit code to use.</param>
        public override void SetShouldExit(int exitCode)
        {
            _exitCode = exitCode;
        }

    }
}
