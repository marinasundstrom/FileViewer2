using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace FileViewer.Tests;

public class NodeTreeBuilderTest
{
    [Fact(Skip = "This does not matter anymore")]
    public async Task Test1()
    {
        var json = await File.ReadAllTextAsync("tree.json");
        var paths = JsonSerializer.Deserialize<IEnumerable<string>>(json);

        NodeTreeBuilder nodeTreeBuilder = new NodeTreeBuilder();

        var rootNode = nodeTreeBuilder.BuildTree(paths!);
    }

    [Fact(DisplayName = "Should create a tree with one child node")]
    public void ShouldCreateATreeWithOneChildNode()
    {
        var paths = new[]
        {
            "/foo",
        };

        NodeTreeBuilder nodeTreeBuilder = new NodeTreeBuilder();

        var rootNode = nodeTreeBuilder.BuildTree(paths!);

        var foo = rootNode.GetChildNode("foo");

        foo.ShouldNotBeNull();

        foo.Name.ShouldBe("foo");

        foo.ChildNodes.ShouldBeEmpty();
    }

    [Fact(DisplayName = "Should create tree with a depth of 2")]
    public void ShouldCreateTreeWithADepthOf2()
    {
        var paths = new[]
        {
            "/foo/bar",
        };

        NodeTreeBuilder nodeTreeBuilder = new NodeTreeBuilder();

        var rootNode = nodeTreeBuilder.BuildTree(paths!);

        rootNode.ChildNodes.Count().ShouldBe(1);

        var foo = rootNode.GetChildNode("foo");

        foo.ShouldNotBeNull();

        foo.Name.ShouldBe("foo");

        foo.ChildNodes.ShouldNotBeEmpty();

        var bar = foo.GetChildNode("bar");

        bar.ShouldNotBeNull();

        bar.Name.ShouldBe("bar");

        bar.ChildNodes.ShouldBeEmpty();

    }

    [Fact(DisplayName = "Should create a tree with two child nodes")]
    public void ShouldCreateATreeWithTwoChildNodes()
    {
        var paths = new[]
        {
            "/first",
            "/second"
        };

        NodeTreeBuilder nodeTreeBuilder = new NodeTreeBuilder();

        var rootNode = nodeTreeBuilder.BuildTree(paths!);

        rootNode.ChildNodes.Count().ShouldBe(2);

        var first = rootNode.GetChildNode("first");
        var second = rootNode.GetChildNode("second");

        first.ShouldNotBeNull();
        second.ShouldNotBeNull();

        first.Name.ShouldBe("first");
        second.Name.ShouldBe("second");

        first.ChildNodes.ShouldBeEmpty();
        second.ChildNodes.ShouldBeEmpty();
    }

    [Fact(DisplayName = "Should create a tree with two child nodes, and a child of one of them")]
    public void ShouldCreateATreeWithTwoChildNodesAndAChildOfOneOfThem()
    {
        var paths = new[]
        {
            "/first",
            "/second/bar"
        };

        NodeTreeBuilder nodeTreeBuilder = new NodeTreeBuilder();

        var rootNode = nodeTreeBuilder.BuildTree(paths!);

        rootNode.ChildNodes.Count().ShouldBe(2);

        var first = rootNode.GetChildNode("first");
        var second = rootNode.GetChildNode("second");

        first.ShouldNotBeNull();
        second.ShouldNotBeNull();

        first.Name.ShouldBe("first");
        second.Name.ShouldBe("second");

        first.ChildNodes.ShouldBeEmpty();
        second.ChildNodes.ShouldNotBeEmpty();

        var bar = second.GetChildNode("bar");

        bar.ShouldNotBeNull();
        bar.Name.ShouldBe("bar");
    }
}