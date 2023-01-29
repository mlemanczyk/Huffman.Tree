using AutoFixture;
using BenchmarkDotNet.Attributes;
using FluentAssertions;
using MoreLinq;

namespace Huffman.Tree.Tests
{
	[TestClass]
	[MemoryDiagnoser]
	public class HuffmanTreeTests
	{
		public readonly record struct TestCase
		{
			public (string token, long frequency)[] Tokens { get; init; }
			public (string token, int[] bits)[] Expected { get; init; }
		}

		private static IEnumerable<TestCase> TestCases => EnumerateTestCases().SelectMany(x => x.OfType<TestCase>());

		private IEnumerable<HuffmanToken<string>>? _codes;
		private (string token, long frequency)[] _tokens;

		public HuffmanTreeTests()
		{
			_tokens = TestCases.First().Tokens;
		}

		[TestMethod]
		[DynamicData(nameof(EnumerateTestCases), DynamicDataSourceType.Method)]
		public void BuildTreeShouldSuccess(TestCase testCase)
		{
			// This is to overcome the requirement for BuildTree() to be argument- & deferred-result-free. It's required by BenchmarkDotNet.
			_tokens = testCase.Tokens;
			BuildTree();
			PrintCodesToOutput();
			ValidateCodes(testCase.Expected);
		}

		private void ValidateCodes((string token, int[] bits)[] expected)
		{
			var expectedBits = expected.Select(x => (x.token, x.bits.Select(bit => Convert.ToBoolean(bit))));
			_ = _codes.Should().NotBeNull();
			_ = _codes!.Select(x => (x.Token, x.Code))
				 .Should()
				 .NotBeNull().And
				 .BeEquivalentTo(expectedBits);
		}

		private void PrintCodesToOutput()
		{
			foreach (var node in _codes!)
			{
				string bits = node.Code!.AsString();
				Console.WriteLine($"{node.Token} => {bits}");
			}
		}

		public static IEnumerable<object[]> EnumerateTestCases()
		{
			yield return new object[]
			{
				new TestCase
				{
					// test case
					Tokens = new[] { ("a", 10L), ("b", 5L), ("c", 2L), ("d", 14L), ("e", 15L), (";", long.MaxValue) },
					// expected result
					Expected = new[] { (";", new[] { 1 }), ("a", new[] { 0, 0, 1 }), ("b", new[] { 0, 0, 0, 1 }), ("c", new[] { 0, 0, 0, 0 }), ("d", new[] { 0, 1, 0 }), ("e", new[] { 0, 1, 1 }) }
				}
			};
		}

		[Benchmark]
		public void BuildTree()
		{
			_codes = _tokens?.AsHuffmanTree().Nodes.OfType<HuffmanToken<string>>();
			// We keep this assertions here because we want to build all the codes. For this we need to access each of them.
			_ = _codes.Should().NotBeNull().And.AllSatisfy(node => node.Code!.AsString().Should().NotBeNull().And.NotBe(string.Empty));
		}

		[Benchmark]
		[TestMethod]
		public void BuildTree100()
		{
			var fixture = new Fixture();
			var collection = fixture.CreateMany<(string token, long frequency)>(100);
			_tokens = collection.ToArray();
			_codes = _tokens?.AsHuffmanTree().Nodes.OfType<HuffmanToken<string>>();
			_codes.ForEach(x => Console.WriteLine($"{x.Token} => {x.Code.AsString()}"));
			// We keep this assertions here because we want to build all the codes. For this we need to access each of them.
			_ = _codes.Should().NotBeNull().And.AllSatisfy(node => node.Code!.AsString().Should().NotBeNull().And.NotBe(string.Empty));
		}
	}
}
