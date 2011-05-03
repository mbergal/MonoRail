﻿//  Copyright 2004-2011 Castle Project - http://www.castleproject.org/
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

namespace Castle.MonoRail

    open System
    open System.Collections.Generic
    open System.Net
    open System.Web
    open Castle.MonoRail.Mvc.ViewEngines

    

    type RedirectResult(url:string) = 
        inherit ActionResult()

        new(url:TargetUrl) = RedirectResult((url.Generate null))

        override this.Execute(context:ActionResultContext) = 
            ignore()


    type HttpResult(status:HttpStatusCode) = 
        inherit ActionResult()
        let _status = status

        override this.Execute(context:ActionResultContext) = 
            ignore()


    type ContentResult<'a>(model:'a) = 
        inherit ActionResult()
        let _model = model
        let mutable _status = HttpStatusCode.OK
        let mutable _redirectTo : TargetUrl = Unchecked.defaultof<_>

        member x.RedirectBrowserTo 
            with get() = _redirectTo and set v = _redirectTo <- v

        member x.StatusCode  
            with get() = _status and set v = _status <- v

        member x.When(``type``:MimeType, perform:Func<ActionResult>) = 
            x

        override this.Execute(context:ActionResultContext) = 
            // context.HttpContext.Request.Acc
            ignore()

        interface IModelAccessor<'a> with 
            member x.Model = _model

        
    type ContentResult() = 
        inherit ContentResult<obj>()

        override this.Execute(context:ActionResultContext) = 
            ignore()


    type ViewResult<'a>(model:'a) = 
        inherit ActionResult()

        let mutable _viewName : string = null
        let mutable _layoutName : string = null
        let mutable _model = model

        member x.ViewName  with get() = _viewName and set v = _viewName <- v
        member x.LayoutName  with get() = _layoutName and set v = _layoutName <- v
        member x.Model  with set v = _model <- v

        override this.Execute(context:ActionResultContext) = 
            let viewreq = new ViewRequest ( 
                                    // AreaName = context.ControllerDescriptor.Area
                                    ViewName = this.ViewName, 
                                    LayoutName = this.LayoutName,
                                    ControllerName = context.ControllerDescriptor.Name, 
                                    ActionName = context.ActionDescriptor.Name
                                )
            let reg = context.ServiceRegistry
            reg.ViewRendererService.Render(viewreq, context.HttpContext, _model)

        interface IModelAccessor<'a> with 
            member x.Model = _model


    type ViewResult() = 
        inherit ViewResult<obj>(obj())


    type JsonResult() = 
        inherit ActionResult()
        override this.Execute(context:ActionResultContext) = 
            ignore()


    type JsResult() = 
        inherit ActionResult()
        override this.Execute(context:ActionResultContext) = 
            ignore()


    type FileResult() = 
        inherit ActionResult()
        override this.Execute(context:ActionResultContext) = 
            ignore()


    type XmlResult() = 
        inherit ActionResult()
        override this.Execute(context:ActionResultContext) = 
            ignore()


