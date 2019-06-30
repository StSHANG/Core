﻿/*
 * This file is part of the CatLib package.
 *
 * (c) CatLib <support@catlib.io>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 *
 * Document: https://catlib.io/
 */

#pragma warning disable CA1031

using CatLib.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CatLib.Tests.Support
{
    [TestClass]
    public class TestsGuard
    {
        [TestMethod]
        public void TestRequires()
        {
            var innerException = new Exception("inner exception");

            try
            {
                Guard.Requires<Exception>(false, "foo", innerException);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("foo", ex.Message);
                Assert.AreEqual("inner exception", ex.InnerException.Message);
            }
        }

        [TestMethod]
        public void TestExtend()
        {
            var innerException = new Exception("inner exception");

            Guard.Extend<ArgumentNullException>((messgae, inner, state) =>
            {
                return new ArgumentNullException("foo", inner);
            });

            try
            {
                Guard.Requires<ArgumentNullException>(false, null, innerException);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("foo", ex.Message);
                Assert.AreEqual("inner exception", ex.InnerException.Message);
            }
        }

        [TestMethod]
        public void TestRequireNotBaseException()
        {
            var innerException = new Exception("inner exception");

            try
            {
                Guard.Requires<ArgumentNullException>(false, "foo", innerException);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("foo", ex.Message);
                Assert.AreEqual("inner exception", ex.InnerException.Message);
            }
        }
    }
}
