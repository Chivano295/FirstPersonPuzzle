using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class NuGetPackageImporter : AssetImporter
{
    [MenuItem("Tools/Experimental/NuGetPackage::ViewAssetInfo")]
    public static void ImIn()
    {
        DefaultAsset obj = Selection.activeObject as DefaultAsset;
        if (obj == null)
        {
            Debug.LogWarning("Select a nuget package file");
            return;
        }
        string path = AssetDatabase.GetAssetPath(obj.GetInstanceID());
        if (!path.EndsWith(".nupkg"))
        {
            Debug.LogWarning("This is not a nuget package file, you idiot, you fool, you absolute buffoon");
            return;
        }
    }
}
#endif

// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
[System.Xml.Serialization.XmlRoot(Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd", IsNullable = false)]
public class Package
{

    private PackageMetadata metadataField;

    /// <remarks/>
    public PackageMetadata metadata
    {
        get
        {
            return this.metadataField;
        }
        set
        {
            this.metadataField = value;
        }
    }
}

/// <remarks/>
[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
public class PackageMetadata
{

    private string idField;

    private string versionField;

    private string titleField;

    private string authorsField;

    private bool requireLicenseAcceptanceField;

    private PackageMetadataLicense licenseField;

    private string licenseUrlField;

    private string iconField;

    private string projectUrlField;

    private string descriptionField;

    private string copyrightField;

    private string tagsField;

    private PackageMetadataRepository repositoryField;

    private PackageMetadataGroup[] dependenciesField;

    private decimal minClientVersionField;

    /// <remarks/>
    public string id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public string version
    {
        get
        {
            return this.versionField;
        }
        set
        {
            this.versionField = value;
        }
    }

    /// <remarks/>
    public string title
    {
        get
        {
            return this.titleField;
        }
        set
        {
            this.titleField = value;
        }
    }

    /// <remarks/>
    public string authors
    {
        get
        {
            return this.authorsField;
        }
        set
        {
            this.authorsField = value;
        }
    }

    /// <remarks/>
    public bool requireLicenseAcceptance
    {
        get
        {
            return this.requireLicenseAcceptanceField;
        }
        set
        {
            this.requireLicenseAcceptanceField = value;
        }
    }

    /// <remarks/>
    public PackageMetadataLicense license
    {
        get
        {
            return this.licenseField;
        }
        set
        {
            this.licenseField = value;
        }
    }

    /// <remarks/>
    public string licenseUrl
    {
        get
        {
            return this.licenseUrlField;
        }
        set
        {
            this.licenseUrlField = value;
        }
    }

    /// <remarks/>
    public string icon
    {
        get
        {
            return this.iconField;
        }
        set
        {
            this.iconField = value;
        }
    }

    /// <remarks/>
    public string projectUrl
    {
        get
        {
            return this.projectUrlField;
        }
        set
        {
            this.projectUrlField = value;
        }
    }

    /// <remarks/>
    public string description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }

    /// <remarks/>
    public string copyright
    {
        get
        {
            return this.copyrightField;
        }
        set
        {
            this.copyrightField = value;
        }
    }

    /// <remarks/>
    public string tags
    {
        get
        {
            return this.tagsField;
        }
        set
        {
            this.tagsField = value;
        }
    }

    /// <remarks/>
    public PackageMetadataRepository repository
    {
        get
        {
            return this.repositoryField;
        }
        set
        {
            this.repositoryField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItem("group", IsNullable = false)]
    public PackageMetadataGroup[] dependencies
    {
        get
        {
            return this.dependenciesField;
        }
        set
        {
            this.dependenciesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public decimal minClientVersion
    {
        get
        {
            return this.minClientVersionField;
        }
        set
        {
            this.minClientVersionField = value;
        }
    }
}

/// <remarks/>
[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
public class PackageMetadataLicense
{

    private string typeField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public string type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlText()]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
public class PackageMetadataRepository
{

    private string typeField;

    private string urlField;

    private string commitField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public string Type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public string url
    {
        get
        {
            return this.urlField;
        }
        set
        {
            this.urlField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public string commit
    {
        get
        {
            return this.commitField;
        }
        set
        {
            this.commitField = value;
        }
    }
}

/// <remarks/>
[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
public class PackageMetadataGroup
{

    private PackageMetadataGroupDependency[] dependencyField;

    private string targetFrameworkField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElement("dependency")]
    public PackageMetadataGroupDependency[] dependency
    {
        get
        {
            return this.dependencyField;
        }
        set
        {
            this.dependencyField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public string targetFramework
    {
        get
        {
            return this.targetFrameworkField;
        }
        set
        {
            this.targetFrameworkField = value;
        }
    }
}

/// <remarks/>
[System.Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
public class PackageMetadataGroupDependency
{

    private string idField;

    private string versionField;

    private string excludeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public string id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public string version
    {
        get
        {
            return this.versionField;
        }
        set
        {
            this.versionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public string exclude
    {
        get
        {
            return this.excludeField;
        }
        set
        {
            this.excludeField = value;
        }
    }
}

