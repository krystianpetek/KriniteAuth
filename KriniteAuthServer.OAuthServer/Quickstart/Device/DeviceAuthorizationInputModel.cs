// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using KriniteAuthServer.OAuthServer.Quickstart.Consent;

namespace KriniteAuthServer.OAuthServer.Quickstart.Device;

public class DeviceAuthorizationInputModel : ConsentInputModel
{
    public string UserCode { get; set; }
}