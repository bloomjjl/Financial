using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace Financial.Tests._DmitriNesteruk
{
    public class Bar : IEquatable<Bar>
    {
        public bool Equals(Bar other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Bar) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        public static bool operator ==(Bar left, Bar right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Bar left, Bar right)
        {
            return !Equals(left, right);
        }

        public string Name { get; set; }
    }

    public interface IBaz
    {
        string Name { get; }
    }

    public interface IFooWithMoq
    {
        bool DoSomething(string value);
        string ProcessString(string value);
        bool TryParse(string value, out string outputValue);
        bool Submit(ref Bar bar);
        int GetCount();
        bool Add(int amount);

        string Name { get; set; }
        IBaz SomeBaz { get; }
        int SomeOtherProperty { get; set; }
    }
    public class FooWithMoq
    {
    }

    public delegate void AlienAbductionEventHandler(int galaxy, bool returned);
    public interface IAnimal
    {
        event EventHandler FallsIll;
        void Stumble();
        event AlienAbductionEventHandler AbductedByAliens;
    }
    public class Doctor
    {
        public int TimesCured;
        public int AbductionsObserved;

        public Doctor(IAnimal animal)
        {
            animal.FallsIll += (sender, args) =>
            {
                Console.WriteLine("I will cure you!");
                TimesCured++;
            };

            animal.AbductedByAliens += (galaxy, returned) => ++AbductionsObserved;
        }
    }

    public class Consumer
    {
        private IFooWithMoq foo;

        public Consumer(IFooWithMoq foo)
        {
            this.foo = foo;
        }

        public void Hello()
        {
            foo.DoSomething("ping");
            var name = foo.Name;
            foo.SomeOtherProperty = 123;
        }
    }

    public abstract class Person
    {
        protected int SSN { get; set; }
        protected abstract void Execute(string cmd);
    }

    

    [TestFixture]
    public class FooWithMoqTests
    {



        // METHODS




        [Test]
        public void SetUpMockMethod_WhenCheckingForStringValue_ReturnTrue_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            mock.Setup(foo => foo.DoSomething("ping"))
                .Returns(true);

            Assert.IsTrue(mock.Object.DoSomething("ping"));
        }

        [Test]
        public void SetUpMockMethod_WhenCheckingForStringValue_ReturnFalse_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            mock.Setup(foo => foo.DoSomething("pong"))
                .Returns(false);

            Assert.IsFalse(mock.Object.DoSomething("pong"));
        }

        [Test]
        public void SetUpMockMethod_WhenCheckingForMultipleStringValues_ReturnFalseForEachValue_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            mock.Setup(foo => foo.DoSomething(It.IsIn("foo", "bar")))
                .Returns(false);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(mock.Object.DoSomething("foo"));
                Assert.IsFalse(mock.Object.DoSomething("bar"));
            });
        }

        [Test]
        public void SetUpMockMethod_WhenCheckingForAnyStringInput_ReturnTrue_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            mock.Setup(foo => foo.DoSomething(It.IsAny<string>()))
                .Returns(true);

            Assert.IsTrue(mock.Object.DoSomething("abc")); 
        }

        [Test]
        public void SetUpMockMethod_WhenCheckingForAnyNumericInput_ReturnFalse_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            mock.Setup(foo => foo.DoSomething(It.IsRegex("[0-9]+")))
                .Returns(false);

            Assert.IsFalse(mock.Object.DoSomething("123")); 
        }

        [Test]
        public void SetUpMockMethod_WhenNumberIsEven_ReturnTrue_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            mock.Setup(foo => foo.Add(It.Is<int>(x => x % 2 == 0)))
                .Returns(true);

            Assert.IsTrue(mock.Object.Add(2));
        }

        [Test]
        public void SetUpMockMethod_WhenNumberIsOdd_ReturnFalse_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            mock.Setup(foo => foo.Add(It.Is<int>(x => x % 2 == 0)))
                .Returns(true);

            Assert.IsFalse(mock.Object.Add(3)); 
        }

        [Test]
        public void SetUpMockMethod_WhenNumberIsWithinRange_ReturnTrue_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            mock.Setup(foo => foo.Add(It.IsInRange<int>(1, 10, Range.Inclusive)))
                .Returns(true);

            Assert.IsTrue(mock.Object.Add(3));
        }

        [Test]
        public void SetUpMockMethod_WhenNumberIsOutsideOfRange_ReturnFalse_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            mock.Setup(foo => foo.Add(It.IsInRange<int>(1, 10, Range.Inclusive)))
                .Returns(true);

            Assert.IsFalse(mock.Object.Add(11));
        }

        [Test]
        public void SetUpMockMethod_WhenCheckingForStringValue_SetOutArgumentAndReturnTrue_Test()
        {
            var mock = new Mock<IFooWithMoq>();
            var requiredOutput = "ok";
            mock.Setup(foo => foo.TryParse("ping", out requiredOutput))
                .Returns(true);

            string result;
            Assert.Multiple(() =>
            {
                Assert.IsTrue(mock.Object.TryParse("ping", out result));
                Assert.That(result, Is.EqualTo(requiredOutput));
            });
        }

        [Test]
        public void SetUpMockMethod_WhenCheckingForInvalidStringValue_SetOutArgumentToNullAndReturnFalse_Test()
        {
            var mock = new Mock<IFooWithMoq>();
            var requiredOutput = "ok";
            mock.Setup(foo => foo.TryParse("ping", out requiredOutput))
                .Returns(true);

            string result;
            Assert.Multiple(() =>
            {
                Assert.IsFalse(mock.Object.TryParse("pong", out result));
                Assert.That(result, Is.EqualTo(null));
            });
        }

        [Test]
        public void SetUpMockMethod_WhenCheckingMultipleStringValuesWithSameOutArgument_ProblemSettingOutArgument_Test()
        {
            var mock = new Mock<IFooWithMoq>();
            var requiredOutput = "ok";
            mock.Setup(foo => foo.TryParse("ping", out requiredOutput))
                .Returns(true);

            string result;
            Assert.Multiple(() =>
            {
                Assert.IsTrue(mock.Object.TryParse("ping", out result));
                Assert.That(result, Is.EqualTo(requiredOutput));

                Assert.IsFalse(mock.Object.TryParse("pong", out result));
                //Assert.That(result, Is.Not.EqualTo(requiredOutput));
                Assert.Warn($"Using the same result field for both [ping] & [pong] can have unexpected results. " +
                            $"[pong] result should be [string.empty] but instead equals [{result}]");
            });
        }

        [Test]
        public void SetupMockMethod_WhenReferenceArgumentIsValid_ReturnTrue_Test()
        {
            var mock = new Mock<IFooWithMoq>();
            var bar = new Bar() {Name="abc"};
            mock.Setup(foo => foo.Submit(ref bar))
                .Returns(true);

            Assert.That(mock.Object.Submit(ref bar), Is.EqualTo(true));
        }

        [Test]
        public void SetupMockMethod_WhenReferenceArgumentIsNotValid_ReturnFalse_Test()
        {
            var mock = new Mock<IFooWithMoq>();
            var bar = new Bar() { Name = "abc" };
            mock.Setup(foo => foo.Submit(ref bar))
                .Returns(true);
            var anotherBar = new Bar() { Name = "def" };

            Assert.IsFalse(mock.Object.Submit(ref anotherBar));
        }

        [Test]
        public void SetupMockMethod_WhenCheckingTwoDifferentReferenceArgumentWithSameName_ProblemReturningUnexpectedBoolean_Test()
        {
            var mock = new Mock<IFooWithMoq>();
            var bar = new Bar() { Name = "abc" };
            mock.Setup(foo => foo.Submit(ref bar))
                .Returns(true);

            var anotherBar = new Bar() { Name = "abc" };
            Assert.Warn("compares reference location, " +
                        "names are the same but for two different reference objects");
            Assert.IsFalse(mock.Object.Submit(ref anotherBar));
        }

        [Test]
        public void SetUpMockMethod_WhenAnyStringValueProvided_ReturnLowerCaseString()
        {
            var mock = new Mock<IFooWithMoq>();

            // setup method that takes input string, formats and returns new string 
            mock.Setup(foo => foo.ProcessString(It.IsAny<string>()))
                .Returns((string s) => s.ToLowerInvariant());

            Assert.That(mock.Object.ProcessString("ABC"),
                Is.EqualTo("abc"));
        }

        [Test]
        public void SetupMockMethod_WhenNoInputsProvidedAndMethodIsCalled_IncrementAndReturnCount_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            // setup method to increment calls when no inputs provided
            var calls = 0;
            mock.Setup(foo => foo.GetCount())
                .Returns(() => calls)
                .Callback(() => ++calls);

            // increment calls
            mock.Object.GetCount();
            mock.Object.GetCount();
            
            Assert.That(mock.Object.GetCount(), Is.EqualTo(2));
        }

        [Test]
        public void SetupMockMethod_WhenStringValueProvided_ReturnException_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            // setup method to throw an exception for input value
            var calls = 0;
            mock.Setup(foo => foo.DoSomething("kill"))
                .Throws<InvalidOperationException>();

            Assert.Throws<InvalidOperationException>(
                    ()=> mock.Object.DoSomething("kill")
                );
        }

        [Test]
        public void SetupMockMethod_WhenStringValueProvidedEqualsNull_ReturnException_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            // setup method to throw an exception for input value
            var calls = 0;
            mock.Setup(foo => foo.DoSomething(null))
                .Throws(new ArgumentException("cmd"));

            Assert.Throws<ArgumentException>(
                () =>
                {
                    mock.Object.DoSomething(null);

                }, "cmd");
        }



        // PROPERTIES



        [Test]
        public void SetupMockProperty_SetNameValue_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            mock.Setup(Foo => Foo.Name)
                .Returns("bar");

            Assert.That(mock.Object.Name, Is.EqualTo("bar"));
        }

        [Test]
        public void SetupMockProperty_WrongWayToSetNameValue_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            mock.Object.Name = "invalid bar";

            Assert.Warn("Name is not set this way, setter not setup yet");
            Assert.That(mock.Object.Name, Is.Not.EqualTo("invalid bar"));
        }

        [Test]
        public void SetupMockProperty_SetPropertyOfAPropertyNameValue_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            mock.Setup(Foo => Foo.SomeBaz.Name)
                .Returns("hello");

            Assert.That(mock.Object.SomeBaz.Name, Is.EqualTo("hello"));
        }
        
        /*
        [Test]
        public void SetupMockProperty_SetPropertySetter_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            var setterCalled = false;
            mock.SetupSet(foo =>
                {
                    foo.Name = It.IsAny<string>();
                })
                .Callback<string>(value =>
                {
                    setterCalled = true;
                });

            IFooWithMoq fooWithMoq = mock.Object;
            fooWithMoq.Name = "def";

            mock.VerifySet(foo =>
            {
                foo.Name = "def";
            }, Times.AtLeastOnce);

            Assert.Multiple(() =>
            {
                Assert.That(mock.Object.Name, Is.EqualTo("def"));
                Assert.IsTrue(setterCalled);
            });
        }
        */

        [Test]
        public void SetupMockProperty_SetUpAllProperties_Test()
        {
            var mock = new Mock<IFooWithMoq>();
            mock.SetupAllProperties();
            IFooWithMoq foo = mock.Object;
            foo.Name = "def";
            foo.SomeOtherProperty = 123;

            Assert.That(mock.Object.Name, Is.EqualTo("def"));
            Assert.That(mock.Object.SomeOtherProperty, Is.EqualTo(123));
        }

        [Test]
        public void SetupMockProperty_SetUpSingleProperty_Test()
        {
            var mock = new Mock<IFooWithMoq>();
            mock.SetupProperty(f => f.Name);
            IFooWithMoq foo = mock.Object;
            foo.Name = "def";
            foo.SomeOtherProperty = 123;

            Assert.That(mock.Object.Name, Is.EqualTo("def"));
            Assert.That(mock.Object.SomeOtherProperty, Is.Not.EqualTo(123));
        }



        // EVENTS



        [Test]
        public void SetupMockEvents_WhenEventRaised_Test()
        {
            var mock = new Mock<IAnimal>();
            var doctor = new Doctor(mock.Object);

            mock.Raise(
                a => a.FallsIll += null, // event action/subscription
                new EventArgs()
            );

            Assert.That(doctor.TimesCured, Is.EqualTo(1));
        }

        [Test]
        public void SetupMockEvents_WhenCalledMethodRaisesEvent_Test()
        {
            var mock = new Mock<IAnimal>();
            var doctor = new Doctor(mock.Object);

            mock.Setup(a => a.Stumble())
                .Raises(a => a.FallsIll += null,
                new EventArgs()
            );

            mock.Object.Stumble();

            Assert.That(doctor.TimesCured, Is.EqualTo(1));
        }

        [Test]
        public void SetupMockEvents_WhenCustomEventRaised_Test()
        {
            var mock = new Mock<IAnimal>();
            var doctor = new Doctor(mock.Object);

            mock.Raise(a => a.AbductedByAliens += null,
                    42, true
                );

            Assert.That(doctor.AbductionsObserved, Is.EqualTo(1));
        }



        // CALLBACKS


        [Test]
        public void CallBack_WhenMethodCalledIncrementCount_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            int x = 0;
            mock.Setup(foo => foo.DoSomething("ping"))
                .Returns(true)
                .Callback(() => x++);
            mock.Object.DoSomething("ping");
            Assert.That(x, Is.EqualTo(1));
        }

        [Test]
        public void CallBack_WhenMethodCalledManipulateArgument_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            int x = 0;
            mock.Setup(foo => foo.DoSomething(It.IsAny<string>()))
                .Returns(true)
                .Callback((string s) => x+= s.Length);
            mock.Object.DoSomething("ping");
            Assert.That(x, Is.EqualTo(4));
        }

        [Test]
        public void CallBack_WhenMethodCalledManipulateArgumentWithGenericOverLoad_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            int x = 0;
            mock.Setup(foo => foo.DoSomething(It.IsAny<string>()))
                .Returns(true)
                .Callback<string>(s => x += s.Length);
            mock.Object.DoSomething("ping");
            Assert.That(x, Is.EqualTo(4));
        }

        [Test]
        public void CallBack_BeforeInvocationOfMethod_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            int x = 0;
            mock.Setup(foo => foo.DoSomething(It.IsAny<string>()))
                .Callback(() => Console.WriteLine("before ping"))
                .Returns(true);
        }

        [Test]
        public void CallBack_AfterInvocationOfMethod_Test()
        {
            var mock = new Mock<IFooWithMoq>();

            int x = 0;
            mock.Setup(foo => foo.DoSomething(It.IsAny<string>()))
                .Returns(true)
                .Callback(() => Console.WriteLine("after ping"));
        }


        // VERIFICATION


        [Test]
        public void Verification_VerifyMethodInvoked_Test()
        {
            var mock = new Mock<IFooWithMoq>();
            var consumer = new Consumer(mock.Object);

            consumer.Hello();

            mock.Verify(foo => foo.DoSomething("ping"), Times.AtLeastOnce);
        }

        [Test]
        public void Verification_VerifyMethodNotInvoked_Test()
        {
            var mock = new Mock<IFooWithMoq>();
            var consumer = new Consumer(mock.Object);

            consumer.Hello();

            mock.Verify(foo => foo.DoSomething("pong"), Times.Never);
        }

        [Test]
        public void Verification_VerifyGetterUsedToAccessVariable_Test()
        {
            var mock = new Mock<IFooWithMoq>();
            var consumer = new Consumer(mock.Object);

            consumer.Hello();

            mock.VerifyGet(foo => foo.Name);
        }

        [Test]
        public void Verification_VerifySetterCalledWithSpecificValue_Test()
        {
            var mock = new Mock<IFooWithMoq>();
            var consumer = new Consumer(mock.Object);

            consumer.Hello();

            mock.VerifySet(foo => foo.SomeOtherProperty = It.IsInRange(100, 200, Range.Inclusive));
        }



        // BEHAVIOR


        [Test]
        public void Behavior_SetupAllInvocationsOnTheMock_Test()
        {
            var mock = new Mock<IFooWithMoq>(MockBehavior.Strict);

            mock.Setup(f => f.DoSomething("abc"))
                .Returns(true);

            mock.Object.DoSomething("abc");
        }

        [Test]
        public void Behavior_AutomaticRecursiveMocking_Test()
        {
            var mock = new Mock<IFooWithMoq>()
            {
                DefaultValue = DefaultValue.Mock
            };

            var baz = mock.Object.SomeBaz;
            var bazMock = Mock.Get(baz);
            bazMock.SetupGet(f => f.Name)
                .Returns("abc");
        }

        [Test]
        public void Behavior_MockRepository_Test()
        {
            var mockRepository = new MockRepository(MockBehavior.Strict)
            {
                DefaultValue = DefaultValue.Mock
            };

            var fooMock = mockRepository.Create<IFooWithMoq>();
            var otherMock = mockRepository.Create<IBaz>(MockBehavior.Loose);

            mockRepository.Verify();
        }



        // PROTECTED MEMBERS



        [Test]
        public void ProtectedMembers_SetupProtectedPropertyToReturnSpecifiedValue_Test()
        {
            var mock = new Mock<Person>();
            /*
            mock.Protected()
                .SetupGet<int>("SSN") // can not use nameof(Person.SSN)
                .Returns(42);
            */
        }

        [Test]
        public void ProtectedMembers_SetupProtectedPropertyWithGenericArgument_Test()
        {
            var mock = new Mock<Person>();
            /*
            mock.Protected()
                .Setup<string>("Execute", ItExpr.IsAny<string>()); // can not use It.IsAny
            */
        }
    }
}
