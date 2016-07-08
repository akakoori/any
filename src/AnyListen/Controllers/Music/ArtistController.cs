﻿using System;
using AnyListen.Api.Music;
using AnyListen.Interface;
using AnyListen.Model;
using Microsoft.AspNetCore.Mvc;

namespace AnyListen.Controllers.Music
{
    [Route("api/[controller]")]
    public class ArtistController : Controller
    {

        [HttpGet("{type}")]
        public ArtistResult Get(string type)
        {
            var id = Request.Query["id"];
            var p = Request.Query["p"];
            var s = Request.Query["s"];
            if (id.Count <= 0)
            {
                return new ArtistResult
                {
                    ErrorCode = 403,
                    ErrorMsg = "请输入艺术家ID"
                };
            }
            if (p.Count <= 0)
            {
                p = "1";
            }
            if (s.Count <= 0)
            {
                s = "30";
            }

            return SearchArtist(type, id[0], p[0], s[0]);
        }

        private static ArtistResult SearchArtist(string type, string id, string page,string size)
        {
            IMusic music;
            switch (type)
            {
                case "wy":
                    music = new WyMusic();
                    break;
                case "xm":
                    music = new XmMusic();
                    break;
                case "tt":
                    music = new TtMusic();
                    break;
                case "qq":
                    music = new TxMusic();
                    break;
                default:
                    return null;
            }
            return music.ArtistSearch(id,Convert.ToInt32(page), Convert.ToInt32(size));
        }

    }
}
