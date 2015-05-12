﻿using Microsoft.SharePoint.Client;

namespace IonFar.SharePoint.Migration
{
    public interface IMigration
    {
        void Up(ClientContext clientContext, IUpgradeLog logger);
    }
}