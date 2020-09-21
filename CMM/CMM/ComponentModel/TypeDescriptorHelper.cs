namespace CMM.ComponentModel
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Threading;

    public static class TypeDescriptorHelper
    {
        private static Hashtable hashtable = new Hashtable();
        private static ReaderWriterLockSlim locker = new ReaderWriterLockSlim();

        public static ICustomTypeDescriptor Get(Type type)
        {
            ICustomTypeDescriptor descriptor2;
            try
            {
                locker.EnterReadLock();
                Type associatedMetadataType = hashtable[type] as Type;
                ICustomTypeDescriptor typeDescriptor = null;
                if (associatedMetadataType != null)
                {
                    typeDescriptor = new AssociatedMetadataTypeTypeDescriptionProvider(type, associatedMetadataType).GetTypeDescriptor(type);
                }
                descriptor2 = typeDescriptor;
            }
            finally
            {
                locker.ExitReadLock();
            }
            return descriptor2;
        }

        public static void RegisterMetadataType(Type type, Type metadataType)
        {
            locker.EnterWriteLock();
            hashtable[type] = metadataType;
            locker.ExitWriteLock();
        }
    }
}

