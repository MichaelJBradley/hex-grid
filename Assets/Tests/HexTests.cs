using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class HexTests
    {
        [Test]
        public void Q_AccessorGetsCorrectValue()
        {
            Hex h = new Hex(3, 4);

            Assert.That(h.Q, Is.EqualTo(3));
        }

        [Test]
        public void Q_MutatorSetsCorrectValue()
        {
            Hex h = new Hex();
            h.Q = 1;
            
            Assert.That(h.Q, Is.EqualTo(1));
        }

        [Test]
        public void R_AccessorGetsCorrectValue()
        {
            Hex h = new Hex(1, 2);

            Assert.That(h.R, Is.EqualTo(2));
        }

        [Test]
        public void R_MutatorSetsCorrectValue()
        {
            Hex h = new Hex();
            h.R = 3;
            
            Assert.That(h.R, Is.EqualTo(3));
        }

        [Test]
        public void S_AccessorGetsCorrectValue()
        {
            Hex h = new Hex(1, 2);

            Assert.That(h.S, Is.EqualTo(-3));
        }

        [Test]
        public void Index_0_GetsQValue()
        {
            Hex h = new Hex(0, 5);

            Assert.That(h[0], Is.EqualTo(0));
        }

        [Test]
        public void Index_1_GetsRValue()
        {
            Hex h = new Hex(4, 7);

            Assert.That(h[1], Is.EqualTo(7));
        }

        [Test]
        public void Index_2_GetsSValue()
        {
            Hex h = new Hex(5, 10);

            Assert.That(h[2], Is.EqualTo(-15));
        }

        [Test]
        public void Index_3_GetIsOutOfRange()
        {
            Hex h = new Hex(2, 4);

            Assert.Throws<IndexOutOfRangeException>(() => { int a = h[3]; });
        }

        [Test]
        public void Index_0_SetsQValue()
        {
            Hex h = new Hex();
            h[0] = 5;
            
            Assert.That(h.Q, Is.EqualTo(5));
        }

        [Test]
        public void Index_1_SetsRValue()
        {
            Hex h = new Hex();
            h[1] = 9;
            
            Assert.That(h.R, Is.EqualTo(9));
        }

        [Test]
        public void Index_2_CannotSetSValue()
        {
            Hex h = new Hex();
            
            Assert.Throws<IndexOutOfRangeException>(() => { h[2] = 5; });
        }

        [Test]
        public void Index_3_SetIsOutOfRange()
        {
            Hex h = new Hex();

            Assert.Throws<IndexOutOfRangeException>(() => { h[3] = 12; });
        }

        [Test]
        public void ConstructorWithInvalidS_IsInvalid()
        {
            Assert.Throws<InvalidHexException>(() => { Hex h = new Hex(1, 2, 3); });
        }

        [Test]
        public void Operator_Equals_TrueForMatchingHexes()
        {
            Hex a = new Hex(1, 3);
            Hex b = new Hex(1, 3);

            Assert.That(a == b, Is.True);
        }

        [Test]
        public void Operator_Equals_FalseForDifferentHexes()
        {
            Hex a = new Hex(2, 5);
            Hex b = new Hex(62, 2);

            Assert.That(a == b, Is.False);
        }

        [Test]
        public void Operator_NotEquals_TrueForDifferentHexes()
        {
            Hex a = new Hex(21, 39);
            Hex b = new Hex(0, 8);

            Assert.That(a != b, Is.True);
        }

        [Test]
        public void Operator_NotEquals_FalseForMatchingHexes()
        {
            Hex a = new Hex(12, 90);
            Hex b = new Hex(12, 90);

            Assert.That(a != b, Is.False);
        }

        [Test]
        public void Operator_Addition_AddsComponents()
        {
            Hex a = new Hex(0, 3);
            Hex b = new Hex(1, 2);

            Assert.That(a + b, Is.EquivalentTo(new Hex(1, 5)));
        }

        [Test]
        public void Operator_Subtraction_SubtractsComponents()
        {
            Hex a = new Hex(6, 12);
            Hex b = new Hex(7, 8);

            Assert.That(a - b, Is.EquivalentTo(new Hex(-1, 4)));
        }

        [Test]
        public void Operator_Multiplication_ScalarMultipliesComponents_ScalarFirst()
        {
            Assert.That(2 * new Hex(3, 1), Is.EquivalentTo(new Hex(6, 2)));
        }

        [Test]
        public void Operator_Multiplication_ScalarMultipliesComponents_ScalarSecond()
        {
            Assert.That(new Hex(9, 8) * 3, Is.EquivalentTo(new Hex(27, 24)));
        }

        [Test]
        public void Operator_Division_ScalarDividesComponents()
        {
            Assert.That(new Hex(12, 27) / 3, Is.EquivalentTo(new Hex(4, 9)));
        }

        [Test]
        public void Operator_Division_DivideByZeroThrowsException()
        {
            Assert.Throws<DivideByZeroException>(() => { Hex h = new Hex(12, 64) / 0; });
        }

        [Test]
        public void Enumerator_IteratesCorrectly()
        {
            Hex h = new Hex(1, 2);
            uint i = 0;
            
            foreach (int x in h)
            {
                Assert.That(x, Is.EqualTo(h[i++]));
            }
        }

        [Test]
        public void LengthIsDeterminedBy_QComponent()
        {
            Hex h = new Hex(-10, 4);
            
            Assert.That(h.Length, Is.EqualTo(10));
        }
        
        [Test]
        public void LengthIsDeterminedBy_RComponent()
        {
            Hex h = new Hex(8, -44);
            
            Assert.That(h.Length, Is.EqualTo(44));
        }
        
        [Test]
        public void LengthIsDeterminedBy_SComponent()
        {
            Hex h = new Hex(15, 2, -17);
            
            Assert.That(h.Length, Is.EqualTo(17));
        }

        [Test]
        public void DistanceIsCalculatedCorrectly()
        {
            Hex a = new Hex(1, 2);
            Hex b = new Hex(3, 4);
            
            Assert.That(Hex.Distance(a, b), Is.EqualTo(4));
        }

        [Test]
        public void DistanceIsCommutative()
        {
            Hex a = new Hex(43, 21);
            Hex b = new Hex(92, 123);
            
            Assert.That(Hex.Distance(a, b), Is.EqualTo(Hex.Distance(b, a)));
        }

        [Test]
        public void CalculateNeighboringHexInThe_PosQ_Direction()
        {
            Hex a = new Hex(4, 5, -9);
            
            Assert.That(a.Neighbor(Direction.PosQ), Is.EquivalentTo(new Hex(5, 5, -10)));
        }

        [Test]
        public void CalculateNeighboringHexInThe_NegQ_Direction()
        {
            Hex a = new Hex(8, -6, -2);
            
            Assert.That(a.Neighbor(Direction.NegQ), Is.EquivalentTo(new Hex(7, -6, -1)));
        }

        [Test]
        public void CalculateNeighboringHexInThe_PosR_Direction()
        {
            Hex a = new Hex(12, -5, -7);
            
            Assert.That(a.Neighbor(Direction.PosR), Is.EquivalentTo(new Hex(11, -4, -7)));
        }

        [Test]
        public void CalculateNeighboringHexInThe_NegR_Direction()
        {
            Hex a = new Hex(9, 15, -24);
            
            Assert.That(a.Neighbor(Direction.NegR), Is.EquivalentTo(new Hex(10, 14, -24)));
        }

        [Test]
        public void CalculateNeighboringHexInThe_PosS_Direction()
        {
            Hex a = new Hex(32, -18, -14);
            
            Assert.That(a.Neighbor(Direction.PosS), Is.EquivalentTo(new Hex(32, -19, -13)));
        }

        [Test]
        public void CalculateNeighboringHexInThe_NegS_Direction()
        {
            Hex a = new Hex(6, 7, -13);
            
            Assert.That(a.Neighbor(Direction.NegS), Is.EquivalentTo(new Hex(6, 8, -14)));
        }

        [Test]
        public void FailingTest()
        {
            Hex a = null;
            
            Assert.That(a is null, Is.False);
        }
    }
}
