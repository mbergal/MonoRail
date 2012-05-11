﻿//  Copyright 2004-2012 Castle Project - http://www.castleproject.org/
//  Hamilton Verissimo de Oliveira and individual contributors as indicated. 
//  See the committers.txt/contributors.txt in the distribution for a 
//  full listing of individual contributors.
// 
//  This is free software; you can redistribute it and/or modify it
//  under the terms of the GNU Lesser General Public License as
//  published by the Free Software Foundation; either version 3 of
//  the License, or (at your option) any later version.
// 
//  You should have received a copy of the GNU Lesser General Public
//  License along with this software; if not, write to the Free
//  Software Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA
//  02110-1301 USA, or see the FSF site: http://www.fsf.org.

namespace Castle.MonoRail.Extension.OData.Serialization

open System
open System.IO
open System.Xml
open System.Data.Services.Providers

module PlainTextSerialization = 
    begin

        type PlainTextSerializer(wrapper, serviceBaseUri, containerUri, rt, propertiesToExpand, writer, enc) as self = 
            class
                inherit Serializer(wrapper, serviceBaseUri, containerUri, rt, propertiesToExpand, writer, enc) 

                let write_primitive_value (rt:ResourceType) (prop:ResourceProperty) value (writer:TextWriter) = 
                    if value = null 
                    then writer.Write "null"
                    else writer.Write (value.ToString())            

                override x.SerializeMany(items) =
                    raise(NotImplementedException())

                override x.SerializeSingle(item) =
                    raise(NotImplementedException())
                
                override x.SerializeProperty(prop:ResourceProperty, value) =
                    write_primitive_value rt prop value writer 

            end

    end