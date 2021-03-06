﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QueueIT.Security.Examples.AspNetMvc.Models;
using QueueIT.Security.Mvc;

namespace QueueIT.Security.Examples.AspNetMvc.Controllers
{
    public class LinkController : Controller
    {
        //
        // GET: /Link/

        public ActionResult Index()
        {
            UriBuilder targetUrl = new UriBuilder(Request.Url);
            targetUrl.Path = "/Link/Target";

            IQueue queue = QueueFactory.CreateQueue("link");

            ViewBag.QueueUrl = queue.GetQueueUrl(targetUrl.Uri.AbsoluteUri);

            return View();
        }

        [KnownUser] // Please be aware that this filter is not applied to error controller actions or similar which will cause users to get looped arround.
        public ActionResult Target(KnownUserModel knownUserModel)
        {
            PersistModel model = new PersistModel(
                knownUserModel.KnownUser.QueueId,
                knownUserModel.KnownUser.PlaceInQueue,
                knownUserModel.KnownUser.TimeStamp);

            model.Persist();

            return View();
        }
    }
}
