﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.Tests
{
    [TestClass()]
    public class WxAppApiTests : CommonApiTest
    {
        [TestMethod()]
        public void CreateWxaQrCodeTest()
        {
            var dt1 = DateTime.Now;
            var ms = new MemoryStream();
            var result = WxAppApi.CreateWxaQrCode(base._appId, ms, "pages/websocket", 100);
            Assert.AreEqual(ReturnCode.请求成功, result.errcode);

            ms.Seek(0, SeekOrigin.Begin);
            //储存图片

            var filePath = "../../Config/qr.jpg";
            File.Delete(filePath);
            using (var fs = new FileStream(filePath, FileMode.CreateNew))
            {
                ms.CopyTo(fs);
                fs.Flush();
            }

            Assert.IsTrue(File.Exists(filePath));
        }

        [TestMethod()]
        public void CreateWxaQrCodeAsyncTest()
        {
            Task.Factory.StartNew(async () =>
            {
                var ms = new MemoryStream();
                var result = await WxAppApi.CreateWxaQrCodeAsync(base._appId, ms, "pages/websocket", 100);
                Assert.AreEqual(ReturnCode.请求成功, result.errcode);

                ms.Seek(0, SeekOrigin.Begin);
                //储存图片
                var filePath = "../../Config/qr-async.jpg";
                File.Delete(filePath);
                using (var fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    await ms.CopyToAsync(fs);
                    await fs.FlushAsync();
                }

                Assert.IsTrue(File.Exists(filePath));
            });
        }
    }
}