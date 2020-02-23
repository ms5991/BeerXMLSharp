# BeerXMLSharp

[![Build Status](https://travis-ci.org/ms5991/BeerXMLSharp.svg?branch=master)](https://travis-ci.org/ms5991/BeerXMLSharp)

This is a C# [BeerXML](http://www.beerxml.com/beerxml.htm) object model and library for serializing/deserializing XML of this format.  It can be used in any .NET application that requires representation of Homebrewing recipies that conform to the open standard.

## Usage
Install the nuget package (TODO).

Per the standard, every element in the BeerXML representation is either a "record" or a "record set".  As such, the object model defines several interfaces:

1. `IBeerXMLEntity`: this is the highest level interface representing any entity that is a BeerXML element.  It defines methods:
  1. GetBeerXML
