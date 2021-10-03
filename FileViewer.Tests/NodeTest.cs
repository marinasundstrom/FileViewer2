using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace FileViewer.Tests;

public class NodeTest
{
    [Fact(DisplayName = "Should not be possible to create a child of a file node")]
    public void ShouldNotBePossibleToCreateAChildOfAFileNode()
    {
        Node node = new Node("file", true);

        Should.Throw<Exception>(() =>
        {
            node.CreateDirectory("directory");
        });
    }

    [Fact(DisplayName = "Should be possible to create children for directory node")]
    public void ShouldBePossiblToCreateChildrenForDirectoryNode()
    {
        Node node = new Node("directory", false);

        node.CreateDirectory("childDir");

        node.CreateFile("childFile");

        var childDir = node.GetChildNode("childDir");

        childDir.ShouldNotBeNull();
        childDir.Name.ShouldBe("childDir");
        childDir.IsDirectory.ShouldBeTrue();
        childDir.Parent.ShouldBe(node);

        var childFile = node.GetChildNode("childFile");

        childFile.ShouldNotBeNull();
        childFile.Name.ShouldBe("childFile");
        childFile.IsFile.ShouldBeTrue();
        childFile.Parent.ShouldBe(node);
    }

    [Fact(DisplayName = "Should be able to detach node from parent")]
    public void ShouldBeAbleToDetachNodeFromParent()
    {
        Node node = new Node("directory", false);

        var childDir = node.CreateDirectory("childDir");

        var childFile = node.CreateFile("childFile");

        childDir.Remove();
        childDir.Parent.ShouldBeNull();

        node.GetChildNode("childDir").ShouldBeNull();

        childFile.Remove();
        childFile.Parent.ShouldBeNull();

        node.GetChildNode("childFile").ShouldBeNull();
    }
}