using System;
using System.Collections.Generic;
using NUnit.Framework;

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

            Assert.That(() => h[3], Throws.TypeOf<IndexOutOfRangeException>());
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

            Assert.That(() => h[2] = 5, Throws.TypeOf<ImmutableHexComponentException>());
        }

        [Test]
        public void Index_3_SetIsOutOfRange()
        {
            Hex h = new Hex();

            Assert.That(() => h[3] = 12, Throws.TypeOf<IndexOutOfRangeException>());
        }

        [Test]
        public void ConstructorWithInvalidS_IsInvalid()
        {
            Assert.That(() => new Hex(1, 2, 3), Throws.TypeOf<InvalidHexException>());
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
        public void Operator_Equals_TrueForBothNull()
        {
            Hex a = null;
            Hex b = null;
            
            Assert.That(a == b, Is.True);
        }

        [Test]
        public void Operator_Equals_FalseForFirstHexNullAndSecondHexNonNull()
        {
            Hex a = null;
            Hex b = new Hex(87, 2);
            
            Assert.That(a == b, Is.False);
        }

        [Test]
        public void Operator_Equals_FalseForFirstHexNonNullAndSecondHexNull()
        {
            Hex a = new Hex(61, 48);
            Hex b = null;
            
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
        public void Operator_Not_TrueForNull()
        {
            Hex h = null;
            
            Assert.That(!h, Is.True);
        }

        [Test]
        public void Operator_Not_FalseForNonNull()
        {
            Hex h = new Hex(1, 2);
            
            Assert.That(!h, Is.False);
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
            Assert.That(() => new Hex(12, 64) / 0, Throws.TypeOf<DivideByZeroException>());
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
        public void Hex_DoesNotEqual_Null()
        {
            Hex h = new Hex();
            
            Assert.That(h.Equals(null), Is.False);
        }

        // This may be a silly test, but I want to make sure different types don't slip past.
        [Test]
        public void Hex_DoesNotEqual_List()
        {
            Hex h = new Hex();
            
            Assert.That(h.Equals(new List<string>()), Is.False);
        }

        [Test]
        public void Hex_Equals_HexWithSameNumbers()
        {
            Hex a = new Hex(9, 1);
            Hex b = new Hex(9, 1);
            
            Assert.That(a.Equals(b), Is.True);
        }

        [Test]
        public void Hex_GetHashCode_IsUnique()
        {
            Hex a = new Hex(10, 43);
            Hex b = new Hex(43, 10);
            
            Assert.That(a.GetHashCode(), Is.Not.EqualTo(b.GetHashCode()));
        }

        [Test]
        public void ToString_HasValidCoordinates_ReturnsCorrectString()
        {
            Hex hex = new Hex(12, 58, -70);
            String expected = "(12, 58, -70)";

            Assert.That(hex.ToString(), Is.EqualTo(expected));
        }
    }
}
