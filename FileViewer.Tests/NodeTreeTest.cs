using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using System.Threading;

namespace FileViewer.Tests;

public class NodeTreeTest
{
    [Fact(DisplayName = "Should signal that a node has been added")]
    public void ShouldSignalThatANodeHasBeenAdded()
    {
        // Arrange

        var rootDir = new Node("root", false);
        var tree = new NodeTree(rootDir);

        AutoResetEvent autoResetEvent = new AutoResetEvent(false);

        Node? parentNode = null;
        Node? addedNode = null;

        tree.NodeAdded += (s, e) =>
        {
            parentNode = e.ParentNode;
            addedNode = e.Node;

            autoResetEvent.Set();
        };

        // Act

        var newDir = rootDir.CreateDirectory("test");

        autoResetEvent.WaitOne();

        // Assert

        parentNode.ShouldBe(rootDir);
        addedNode.ShouldBe(newDir);
    }

    [Fact(DisplayName = "Should signal that a node has been removed")]
    public void ShouldSignalThatANodeHasBeenRemoved()
    {
        // Arrange

        var rootDir = new Node("root", false);
        var tree = new NodeTree(rootDir);

        var newDir = rootDir.CreateDirectory("test");

        AutoResetEvent autoResetEvent = new AutoResetEvent(false);

        Node? parentNode = null;
        Node? removedDir = null;

        tree.NodeRemoved += (s, e) =>
        {
            parentNode = e.ParentNode;
            removedDir = e.Node;

            autoResetEvent.Set();
        };

        // Act

        newDir.Remove();

        autoResetEvent.WaitOne();

        // Assert

        parentNode.ShouldBe(rootDir);
        removedDir.ShouldBe(newDir);
    }

    [Fact(DisplayName = "Should signal that a node has been renamed")]
    public void ShouldSignalThatANodeHasBeenRenamed()
    {
        // Arrange

        var rootDir = new Node("root", false);
        var tree = new NodeTree(rootDir);

        string actualOldName = "test";

        var newDir = rootDir.CreateDirectory(actualOldName);

        AutoResetEvent autoResetEvent = new AutoResetEvent(false);

        Node? node = null;
        string? oldName = null;
        string? newName = null;

        tree.NodeRenamed += (s, e) =>
        {
            node = e.Node;
            oldName = e.OldName;
            newName = e.NewName;

            autoResetEvent.Set();
        };

        // Act

        var actualNewName = "bob";

        newDir.Rename(actualNewName) ;

        autoResetEvent.WaitOne();

        // Assert

        node.ShouldBe(newDir);
        oldName.ShouldBe(actualOldName);
        newName.ShouldBe(actualNewName);
    }
}