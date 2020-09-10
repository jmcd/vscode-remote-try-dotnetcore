/*----------------------------------------------------------------------------------------
 * Copyright (c) Microsoft Corporation. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *---------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

/* Top Level Statement */

Host.CreateDefaultBuilder()
.ConfigureWebHostDefaults(webBuilder =>
{
    webBuilder.Configure(app =>
    {
        app.UseHttpsRedirection()
            .Run(async context =>
            {
                await context.Response.WriteAsync("Hello remote world from ASP.NET Core!");
            });
    });
}).Build().Run();

class InitOnlyProperties
{
    public class Person
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public int Age { get; set; }
    }

    void UseCase()
    {
        var alice = new Person { FirstName = "Alice", Age = 50 };
        // alice.LastName = "Apple"; won't compile
        alice.Age = 51;
    }
}

class Records
{

    public record Person
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }

    void UseCase()
    {
        // Record is immutable
        var alice = new Person { FirstName = "Alice", LastName = "Apple" };

        // Take a copy, with modification
        var alice2 = alice with { LastName = "Banana" };
    }
}

class PatterMatching
{
    void UseCase_PropertyPatterns()
    {
        var context = (IsReachable: true, Length: 99, Name: "Foo");

        if (context is { IsReachable: true, Length: > 1 })  // equiv to: if (context is object && context.IsReachable && context.Length > 1 ) and if (context?.IsReachable && context?.Length > 1 )
        {
            Console.WriteLine(context.Name);
        }
    }

    /* TODO: not sure how to use this
    class DeliveryTruck{
        Decimal GrossWeightClass;
    }

        void UseCase_RelationalPatterns() {



            DeliveryTruck t when t.GrossWeightClass switch
    {
        < 3000 => 8.00m,
        >= 3000 and <= 5000 => 10.00m,
        > 5000 => 15.00m,
    },

        }*/
}

class TargetTypedNew
{
    class Point
    {
        public Point(int x, int y)
        { }
    }

    void UseCase()
    {
        List<string> values = new();

        Point p = new(3, 5);
    }
}

class CovariantReturns
{
    class Food { }
    class Meat : Food { }

    abstract class Animal
    {
        public abstract Food GetFood();
    }

    class Tiger : Animal
    {
        public override Meat GetFood() => new Meat();
    }
}