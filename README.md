# DataAccess.EventHandler Toolbox

Dataaccess eventhandler

## Table of Contents

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Installation](#installation)
- [Configuration in Startup.Configure](#configuration-in-startupconfigure)
- [What's under the hood?](#whats-under-the-hood)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Installation

To add the toolbox to a project, you add the package to the project.json :

``` json
"dependencies": {
    "Digipolis.Toolbox.DataAccess.EventHandler":  "1.0.2"
 }
```

In Visual Studio you can also use the NuGet Package Manager to do this.

This Toolbox depends on "Digipolis.Toolbox.EventHandler" so you need to include and configure this as well !

## Configuration in Startup.Configure

The Event toolbox is registered in the _**Configure**_ method of the *Startup* class. (Remember to setup the configureservices part of the Digipolis.Toolbox.EventHandler package as well!)

``` csharp
    app.UseDataAccessEventHandler();

```  

## What's under the hood?
This toolbox basically registers an IDbCommandInterceptor for Entity framework.
It then intercepts Create, Update, and Delete (Add, Modify, Delete) operations on entities and sends out an event with the topic 'CUD' to the event handler endpoint you specify.
