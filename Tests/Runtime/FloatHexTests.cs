using System;
using NUnit.Framework;

namespace Tests
{
    public class FloatHexTests
    {
        [Test]
        public void Q_AccessorGetsCorrectValue()
        {
            FloatHex h = new FloatHex(2.3f, 9.2f);

            Assert.That(h.Q, Is.EqualTo(2.3f));
        }

        [Test]
        public void Q_MutatorSetsCorrectValue()
        {
            FloatHex h = new FloatHex();
            h.Q = 6.631f;

            Assert.That(h.Q, Is.EqualTo(6.631f));
        }

        [Test]
        public void R_AccessorGetsCorrectValue()
        {
            FloatHex h = new FloatHex(5.12f, 86.99f);

            Assert.That(h.R, Is.EqualTo(86.99f));
        }
        
        [Test]
        public void R_MutatorSetsCorrectValue()
        {
            FloatHex h = new FloatHex();
            h.R = 83.123f;

            Assert.That(h.R, Is.EqualTo(83.123f));
        }

        [Test]
        public void S_AccessorGetsCorrectValue()
        {
            FloatHex h = new FloatHex(1.2f, 0);

            Assert.That(h.S, Is.EqualTo(-1.2f));
        }

        [Test]
        public void Index_0_GetsQValue()
        {
            FloatHex h = new FloatHex(15.72f, 1034.2f);

            Assert.That(h[0], Is.EqualTo(15.72f));
        }

        [Test]
        public void Index_1_GetsRValue()
        {
            FloatHex h = new FloatHex(834.06f, 788.1f);

            Assert.That(h[1], Is.EqualTo(788.1f));
        }

        [Test]
        public void Index_2_GetsSValue()
        {
            FloatHex h = new FloatHex(0.45f, 699.00f);

            Assert.That(h[2], Is.EqualTo(-699.45f));
        }

        [Test]
        public void Index_3_Get_ThrowsIndexOutOfRangeException()
        {
            FloatHex h = new FloatHex();

            Assert.That(() => h[3], Throws.TypeOf<IndexOutOfRangeException>());
        }

        [Test]
        public void Index_0_SetsQValue()
        {
            FloatHex h = new FloatHex();
            h[0] = 16.64f;

            Assert.That(h[0], Is.EqualTo(16.64f));
        }

        [Test]
        public void Index_1_SetsRValue()
        {
            FloatHex h = new FloatHex();
            h[1] = 64.256f;

            Assert.That(h[1], Is.EqualTo(64.256f));
        }

        [Test]
        public void Index_2_Set_ThrowsImmutableHexComponentException()
        {
            FloatHex h = new FloatHex();

            Assert.That(() => h[2] = 13.33f, Throws.TypeOf<ImmutableHexComponentException>());
        }

        [Test]
        public void Index_3_Set_ThrowsIndexOutOfRangeException()
        {
            FloatHex h = new FloatHex();

            Assert.That(() => h[3] = 0.01f, Throws.TypeOf<IndexOutOfRangeException>());
        }

        [Test]
        public void Constructor_WithInvalidS_ThrowsInvalidHexException()
        {
            Assert.That(() => new FloatHex(1.11f, 2.22f, 3.33f), Throws.TypeOf<InvalidHexException>());
        }

        [Test]
        public void HexToFloatHexConstructor_SetsComponentsCorrectly()
        {
            Hex a = new Hex(9, 10);
            FloatHex b = new FloatHex(a);

            Assert.That(b.Q, Is.EqualTo(9.0f));
            Assert.That(b.R, Is.EqualTo(10.0f));
        }

        [Test]
        public void OperatorEquals_WithEquivalentComponents_ReturnsTrue()
        {
            FloatHex a = new FloatHex(6.78f, 9.12f);
            FloatHex b = new FloatHex(6.78f, 9.12f);

            Assert.That(a == b, Is.True);
        }

        [Test]
        public void OperatorEquals_WithDifferentComponents_ReturnsFalse()
        {
            FloatHex a = new FloatHex();
            FloatHex b = new FloatHex(100.001f, 69.96f);

            Assert.That(a == b, Is.False);
        }

        [Test]
        public void OperatorEquals_WithBothFloatHexesNull_ReturnsTrue()
        {
            FloatHex a = null;
            FloatHex b = null;

            Assert.That(a == b, Is.True);
        }

        [Test]
        public void OperatorEquals_WithFirstFloatHexNull_ReturnsFalse()
        {
            FloatHex a = null;
            FloatHex b = new FloatHex();

            Assert.That(a == b, Is.False);
        }

        [Test]
        public void OperatorEquals_WithSecondFloatHexNull_ReturnsFalse()
        {
            FloatHex a = new FloatHex(3.21f, 0.11f);
            FloatHex b = null;

            Assert.That(a == b, Is.False);
        }

        [Test]
        public void OperatorNotEquals_WithEquivalentComponents_ReturnsFalse()
        {
            FloatHex a = new FloatHex();
            FloatHex b = new FloatHex();

            Assert.That(a != b, Is.False);
        }

        [Test]
        public void OperatorNotEquals_WithDifferentComponents_ReturnsTrue()
        {
            FloatHex a = new FloatHex(6.78f, 12.13f);
            FloatHex b = new FloatHex(832.83f, 857203.28482f);

            Assert.That(a != b, Is.True);
        }

        [Test]
        public void OperatorNot_WithNullFloatHex_ReturnsTrue()
        {
            FloatHex a = null;

            Assert.That(!a, Is.True);
        }

        [Test]
        public void OperatorNot_WithNonNullFloatHex_ReturnsFalse()
        {
            FloatHex a = new FloatHex();

            Assert.That(!a, Is.False);
        }

        [Test]
        public void OperatorAddition_DoesComponentWiseAddition()
        {
            FloatHex a = new FloatHex(9.1f, 7.8f);
            FloatHex b = new FloatHex(1.6f, 2.2f);

            Assert.That(a + b, Is.EqualTo(new FloatHex(10.7f, 10.0f)).Within(.0001f));
        }
        
        [Test]
        public void OperatorAddition_WithHex_DoesImplicitConversion()
        {
            FloatHex a = new FloatHex(12.1f, -10.3f, -1.8f);
            Hex b = new Hex(-3, 5, -2);

            Assert.That(a + b, Is.EqualTo(new FloatHex(9.1f, -5.3f, -3.8f)).Within(.0001f));
        }

        [Test]
        public void OperatorSubtraction_DoesComponentWiseSubtration()
        {
            FloatHex a = new FloatHex(12.4f, 265.9f);
            FloatHex b = new FloatHex(16.99f, 24.125f);

            Assert.That(a - b, Is.EqualTo(new FloatHex(-4.59f, 241.775f)).Within(.0001f));
        }

        [Test]
        public void OperatorSubtraction_WithHex_DoesImplicitConversion()
        {
            Hex a = new Hex(1, -4, 3);
            FloatHex b = new FloatHex(-2.4f, 8.9f, -6.5f);

            Assert.That(a - b, Is.EqualTo(new FloatHex(3.4f, -12.9f, 9.5f)).Within(.0001f));
        }

        [Test]
        public void OperatorMultiplication_WithScalarFirst_DoesScalarMultiplication()
        {
            float f = 8.1f;
            FloatHex h = new FloatHex(10.75f, 80.23f);

            Assert.That(f * h, Is.EqualTo(new FloatHex(87.075f, 649.863f)).Within(.0001f));
        }

        [Test]
        public void OperatorMultiplication_WithFloatHexFirst_DoesScalarMultiplication()
        {
            FloatHex h = new FloatHex(90.11f, 82.8f);
            float f = 17.3f;

            Assert.That(h * f, Is.EqualTo(new FloatHex(1558.903f, 1432.44f)).Within(.0001f));
        }

        [Test]
        public void OperatorDivision_DoesScalarDivision()
        {
            FloatHex h = new FloatHex(15.6f, 90.23f);
            float f = 5.25f;

            Assert.That(h / f, Is.EqualTo(new FloatHex(2.9714f, 17.1866f)).Within(.0001f));
        }

        [Test]
        public void OperatorDivision_WithZeroScalar_ThrowsDivideByZeroException()
        {
            FloatHex h = new FloatHex(67.456f, 4598.12f);

            Assert.That(() => h / 0f, Throws.TypeOf<DivideByZeroException>());
        }

        [Test]
        public void OperatorExplicitFloatHexToHex_ConvertsCorrectly()
        {
            FloatHex h = new FloatHex(7.2f, -1.1f, -6.1f);

            Assert.That((Hex)h, Is.EqualTo(new Hex(7, -1, -6)));
        }

        [Test]
        public void Enumerator_IteratesCorrectly()
        {
            FloatHex h = new FloatHex(5.3f, 99.0f);
            uint i = 0;
            
            foreach (float x in h)
            {
                Assert.That(x, Is.EqualTo(h[i++]));
            }
        }

        [Test]
        public void Length_WithQComponentMax_IsDeterminedByQ()
        {
            FloatHex h = new FloatHex(5.69f, -4.1f);

            Assert.That(h.Length, Is.EqualTo(5.69f));
        }

        [Test]
        public void Length_WithRComponentMax_IsDeterminedByR()
        {
            FloatHex h = new FloatHex(6.01f, -10.44f);

            Assert.That(h.Length, Is.EqualTo(10.44f));
        }

        [Test]
        public void Length_WithSComponentMax_IsDeterminedByS()
        {
            FloatHex h = new FloatHex(-4.97f, -1.55f, 6.52f);

            Assert.That(h.Length, Is.EqualTo(6.52f).Within(.0001f));
        }

        [Test]
        public void Length_WithNegativeSComponentMax_IsDeterminedByS()
        {
            FloatHex h = new FloatHex(4.97f, 1.55f, -6.52f);

            Assert.That(h.Length, Is.EqualTo(6.52).Within(.0001f));
        }

        [Test]
        public void Round_WithQDiffLargest_CalculatesCorrectly()
        {
            // Diffs
            // Q: .39
            // R: .37
            // S: .24
            FloatHex h = new FloatHex(-8.61f, 9.37f, -.76f);

            Assert.That(h.Round(), Is.EqualTo(new Hex(-8, 9, -1)));
        }

        [Test]
        public void RoundWithRDiffLargest_CalculatesCorrectly()
        {
            // Diffs
            // Q: .1
            // R: .47
            // S: .43
            FloatHex h = new FloatHex(4.1f, 7.47f, -11.57f);

            Assert.That(h.Round(), Is.EqualTo(new Hex(4, 8, -12)));
        }

        [Test]
        public void Round_WithSDiffLargest_CalculatesCorrectly()
        {
            // Diffs
            // Q: .31
            // R: .21
            // S: .48
            FloatHex h = new FloatHex(-3.31f, -2.21f, 5.52f);

            Assert.That(h.Round(), Is.EqualTo(new Hex(-3, -2, 5)));
        }

        [Test]
        public void Distance_CalculatesCorrectly()
        {
            FloatHex a = new FloatHex(65.63f, 31.67f);
            FloatHex b = new FloatHex(-88.41f, 984.45f);

            Assert.That(FloatHex.Distance(a, b), Is.EqualTo(952.78f));
        }

        [Test]
        public void Distance_IsCommutative()
        {
            FloatHex a = new FloatHex(12.64f, 4958.934f);
            FloatHex b = new FloatHex(9834.009f, 238.24f);

            Assert.That(FloatHex.Distance(a, b), Is.EqualTo(FloatHex.Distance(b, a)));
        }

        [Test]
        public void Neighbor_InThePosQDirection_CalculatesCorrectly()
        {
            FloatHex h = new FloatHex(5478.05f, 1234.56f, -6712.61f);

            // Bigger floats require bigger delta
            Assert.That(h.Neighbor(Direction.PosQ), Is.EqualTo(new Hex(5479, 1235, -6714)));
        }

        [Test]
        public void Neighbor_InTheNegQDirection_CalculatesCorrectly()
        {
            FloatHex h = new FloatHex(1.4f, -2.5f, 1.1f);
            
            Assert.That(h.Neighbor(Direction.NegQ), Is.EqualTo(new Hex(0, -2, 2)));
        }

        [Test]
        public void Neighbor_InThePosRDirection_CalculatesCorrectly()
        {
            FloatHex h = new FloatHex(-7.12f, 18.2f, -11.08f);

            Assert.That(h.Neighbor(Direction.PosR), Is.EqualTo(new Hex(-8, 19, -11)));
        }

        [Test]
        public void Neighbor_InTheNegRDirection_CalculatesCorrectly()
        {
            FloatHex h = new FloatHex(5.55f, 9.99f, -15.54f);

            Assert.That(h.Neighbor(Direction.NegR), Is.EqualTo(new Hex(7, 9, -16)));
        }

        [Test]
        public void Neighbor_InThePosSDirection_CalculatesCorrectly()
        {
            FloatHex h = new FloatHex(-8.001f, 7.00f, 1.001f);

            Assert.That(h.Neighbor(Direction.PosS), Is.EqualTo(new Hex(-8, 6, 2)));
        }

        [Test]
        public void Neighbor_InTheNegSDirection_CalculatesCorrectly()
        {
            FloatHex h = new FloatHex(-1.8f, 1.8f, 0.0f);

            Assert.That(h.Neighbor(Direction.NegS), Is.EqualTo(new Hex(-2, 3, -1)));
        }

        [Test]
        public void Equals_WithNull_ReturnsFalse()
        {
            FloatHex h = new FloatHex();

            Assert.That(h.Equals(null), Is.False);
        }

        [Test]
        public void Equals_WithDifferentType_ReturnsFalse()
        {
            FloatHex h = new FloatHex();

            Assert.That(h.Equals("This ain't a Hex"), Is.False);
        }

        [Test]
        public void Equals_WithSameComponentValues_ReturnsTrue()
        {
            FloatHex a = new FloatHex(6.33f, 9.42f);
            FloatHex b = new FloatHex(6.33f, 9.42f);

            Assert.That(a.Equals(b), Is.True);
        }

        [Test]
        public void Equals_WithComponentValuesWithinDelta_ReturnsTrue()
        {
            FloatHex a = new FloatHex(10.5832f, -23.7743f);
            FloatHex b = new FloatHex(10.58315f, -23.77433f);

            Assert.That(a.Equals(b, .0001f), Is.True);
        }

        [Test]
        public void Equals_WithQComponentValueOutsideDelta_ReturnsFalse()
        {
            FloatHex a = new FloatHex(-17.862104f, -9.223308f);
            FloatHex b = new FloatHex(-17.862106f, -9.223308f);

            Assert.That(a.Equals(b), Is.False);
        }

        [Test]
        public void GetHashCode_WithSameNumbersReversed_IsUnique()
        {
            FloatHex a = new FloatHex(8.88f, 11.99f);
            FloatHex b = new FloatHex(11.99f, 8.88f);

            Assert.That(a.GetHashCode(), Is.Not.EqualTo(b.GetHashCode()));
        }

        [Test]
        public void ToString_HasValidCoordinates_ReturnsCorrectString()
        {
            FloatHex hex = new FloatHex(54.23f, -9.22f, -45.01f);
            String expected = "(54.23, -9.22, -45.01)";

            Assert.That(hex.ToString(), Is.EqualTo(expected));
        }
    }
}
