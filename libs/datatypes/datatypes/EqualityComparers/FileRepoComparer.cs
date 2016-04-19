using System.Collections.Generic;
using DataTypes.Models;

namespace DataTypes.EqualityComparers
    {
    public class FileRepoComparer : IEqualityComparer<FileRepo>
        {
        public bool Equals(FileRepo x, FileRepo y)
        {
            return x.GuidFile.Equals(y.GuidFile);
        }

        public int GetHashCode(FileRepo obj)
        {
            /*
                       If you just return 0 for the hash the Equals comparer will kick in. 
                       The underlying evaluation checks the hash and then short circuits the evaluation if it is false.
                       Otherwise, it checks the Equals. If you force the hash to be true (by assuming 0 for both objects), 
                       you will always fall through to the Equals check which is what we are always going for.
                      */
            return 0;

            }
        }
    }