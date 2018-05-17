using UnityEngine.Assertions;
//using Xunit;

public class MyStackTests {
//    [Fact]
    public void PopTest() {
        var stack = new MyStack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);

        var temp = stack.Pop();
//        Assert.True(temp == 3);
    }
    //todo 尝试单元测试失败
}
