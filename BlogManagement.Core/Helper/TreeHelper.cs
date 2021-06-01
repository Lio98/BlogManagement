using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogManagement.Core
{
    /// <summary>
    /// 树结构帮助类
    /// </summary>
    public static class TreeHelper
    {
        /// <summary>
        /// 建造树结构
        /// </summary>
        /// <param name="allNodes">所有的节点</param>
        /// <param name="parentId">节点</param>
        /// <returns></returns>
        public static List<T> ToTree<T>(this List<T> allNodes, string parentId = "0") where T : TreeModel, new()
        {
            List<T> resData = new List<T>();
            var rootNodes = allNodes.Where(x => x.parentId == parentId || x.parentId.IsNullOrEmpty()).ToList();
            resData = rootNodes;
            resData.ForEach(aRootNode =>
            {
                aRootNode.hasChildren = HaveChildren(allNodes, aRootNode.id);
                if (aRootNode.hasChildren)
                    aRootNode.children = _GetChildren(allNodes, aRootNode);
            });
            return resData;
        }
        #region 私有成员
        /// <summary>
        /// 获取所有子节点
        /// </summary>
        /// <typeparam name="T">树模型（TreeModel或继承它的模型）</typeparam>
        /// <param name="nodes">所有节点列表</param>
        /// <param name="parentNode">父节点Id</param>
        /// <returns></returns>
        private static List<object> _GetChildren<T>(List<T> nodes, T parentNode) where T : TreeModel, new()
        {
            Type type = typeof(T);
            var properties = type.GetProperties().ToList();
            List<object> resData = new List<object>();
            var children = nodes.Where(x => x.parentId == parentNode.id).ToList();
            children.ForEach(aChildren =>
            {
                T newNode = new T();
                resData.Add(newNode);
                //赋值属性
                foreach (var aProperty in properties.Where(x => x.CanWrite))
                {
                    var value = aProperty.GetValue(aChildren, null);
                    aProperty.SetValue(newNode, value);
                }
                newNode.hasChildren = HaveChildren(nodes, aChildren.id);
                if (newNode.hasChildren)
                    newNode.children = _GetChildren(nodes, newNode);
            });

            return resData;
        }
        /// <summary>
        /// 判断当前节点是否有子节点
        /// </summary>
        /// <typeparam name="T">树模型</typeparam>
        /// <param name="nodes">所有节点</param>
        /// <param name="nodeId">当前节点Id</param>
        /// <returns></returns>
        private static bool HaveChildren<T>(List<T> nodes, string nodeId) where T : TreeModel, new()
        {
            return nodes.Exists(x => x.parentId == nodeId);
        }
        #endregion
    }
    public class TreeModel
    {
        public string id { get; set; }
        public string parentId { get; set; }
        public bool hasChildren { get; set; }
        public List<object> children { get; set; }
    }
}
