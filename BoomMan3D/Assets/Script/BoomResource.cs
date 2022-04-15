using System.Collections;
using UnityEngine;
namespace BoomResource
{
    public sealed class PrefabsResource
    {

        private static PrefabsResource instance = null;
        private static readonly object padlock = new object();
        private ArrayList resourceArr;//偶数位为path，下一位为resources
        PrefabsResource()
        {
            this.resourceArr =  new ArrayList();
        }

        public Object LoadResource(string path)
        {
            Object resource=getResource(path);
            Debug.Log(path);
            if(resource!=null){
                return resource;
            }
            this.resourceArr.Add(path);
            resource = Resources.Load(path);
            this.resourceArr.Add(resource);
            return resource;
        }
         
        private Object getResource(string path){
            for(int i=0;i<this.resourceArr.Count;i+=2){
                if((string)this.resourceArr[i]==path){
                    return (Object)this.resourceArr[i+1];
                }
            }
            return null;
        }

        public static PrefabsResource Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new PrefabsResource();
                    }
                    return instance;
                }

            }
        }
    }
}