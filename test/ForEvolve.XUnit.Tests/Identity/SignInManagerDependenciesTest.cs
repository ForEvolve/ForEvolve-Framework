using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ForEvolve.XUnit.Identity
{
    public class SignInManagerDependenciesTest
    {
        [Fact]
        public void Should_be_instantiable()
        {
            var helper = new SignInManagerDependencies<FakeUser>();
        }
    }
}
