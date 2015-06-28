////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.IO;

namespace FileSystemTest
{
    public class Copy : IMFTestInterface
    {
        [SetUp]
        public InitializeResult Initialize()
        {
            Log.Comment("Adding set up for the tests.");
            try
            {
                IOTests.IntializeVolume();

                Directory.CreateDirectory(sourceDir);
                Directory.CreateDirectory(destDir);
                Directory.SetCurrentDirectory(sourceDir);

                file1 = new FileInfo(file1Name);
                file2 = new FileInfo(file2Name);
            }
            catch (Exception ex)
            {
                Log.Comment("Skipping: Unable to initialize file system" + ex.Message);
                return InitializeResult.Skip;
            }
            // Add your functionality here.                

            return InitializeResult.ReadyToGo;
        }

        [TearDown]
        public void CleanUp()
        {
            Log.Comment("Cleaning up after the tests.");

            // TODO: Add your clean up steps here.
        }


        #region Local vars
        private const string file1Name = "file1.tmp";
        private const string file2Name = "file2.txt";
        private const string sourceDir = "source";
        private const string destDir = "destination";
        private FileInfo file1;
        private FileInfo file2; 
        #endregion Local vars

        #region Helper methods
        private bool TestCopy(string source, string destination)
        {
            bool success = true;
            Log.Comment("Copy " + source + " to " + destination);
            try
            {
                success = CreateFile(source);
                if (success)
                {
                    File.Copy(source, destination);
                    if (!VerifyExist(source))
                        success = false;
                    if (!VerifyExist(destination))
                        success = false;
                }
                else
                {
                    Log.Exception("Unable to create file!");
                }
            }
            finally
            {
                File.Delete(source);
                File.Delete(destination);
            }
            return success;
        }

        private bool TestCopy(string source, string destination, bool overwrite)
        {
            bool success = true;
            Log.Comment("Copy " + source + " to " + destination + " with overwrite " + overwrite);
            try
            {
                success = CreateFile(source);
                success = CreateFile(destination);
                if (success)
                {
                    try
                    {
                        File.Copy(source, destination, overwrite);
                    }
                    catch (IOException ex)
                    {
                        if (overwrite)
                        {
                            Log.Exception("Unexpected exception when overwrite set to true");
                            throw ex;
                        }
                    }
                    success = VerifyExist(source);
                    success = VerifyExist(destination);
                }
                else
                {
                    Log.Exception("Unable to create file!");
                }
            }
            finally
            {
                File.Delete(source);
                File.Delete(destination);
            }
            return success;
        }

        private bool CreateFile(string file)
        {
            File.Create(file).Close();
            return VerifyExist(file);
        }

        private bool VerifyExist(string file)
        {
            FileInfo fileinfo = new FileInfo(file);
            if (fileinfo.Exists)
                return true;
            Log.Exception("File does not exist: " + file);
            return false;
        }
        
        #endregion Helper methods

        #region Test Cases

        [TestMethod]
        public MFTestResults ArgumentExceptionTests()
        {
            MFTestResults result = MFTestResults.Pass;
            try
            {
                Log.Comment(Directory.GetCurrentDirectory());
                try
                {
                    File.Create(file1Name).Close();
                    Log.Comment("Null Source Constructor");
                    File.Copy(null, file2.Name);
                    Log.Exception( "Expected ArgumentException" );
                    return MFTestResults.Fail;
                }
                catch(ArgumentException ae)
                { /* pass case */
                    Log.Comment( "Got correct exception: " + ae.Message );
                    result = MFTestResults.Pass;
                }
                finally { File.Delete(file1Name); }
                try
                {
                    File.Create(file1Name).Close();
                    Log.Comment("Null Destination Constructor");
                    File.Copy(file1.Name, null);
                    Log.Exception( "Expected ArgumentException" );
                    return MFTestResults.Fail;
                }
                catch(ArgumentException ae1)
                { /* pass case */
                    Log.Comment( "Got correct exception: " + ae1.Message );
                    result = MFTestResults.Pass;
                }
                finally { File.Delete(file1Name); }

                try
                {
                    File.Create(file1Name).Close();
                    Log.Comment("String.Empty Source Constructor");
                    File.Copy(string.Empty, file2.Name);
                    Log.Exception( "Expected ArgumentException" );
                    return MFTestResults.Fail;
                }
                catch(ArgumentException ae2)
                { /* pass case */
                    Log.Comment( "Got correct exception: " + ae2.Message );
                    result = MFTestResults.Pass;
                }
                finally { File.Delete(file1Name); }
                try
                {
                    File.Create(file1Name).Close();
                    Log.Comment("String.Empty Destination Constructor");
                    File.Copy(file1.Name, string.Empty);
                    Log.Exception( "Expected ArgumentException" );
                    return MFTestResults.Fail;
                }
                catch(ArgumentException ae3)
                { /* pass case */
                    Log.Comment( "Got correct exception: " + ae3.Message );
                    result = MFTestResults.Pass;
                }
                finally { File.Delete(file1Name); }

                try
                {
                    File.Create(file1Name).Close();
                    Log.Comment("Whitespace Source Constructor");
                    File.Copy("       ", file2.Name);
                    Log.Exception( "Expected ArgumentException" );
                    return MFTestResults.Fail;
                }
                catch(ArgumentException ae4)
                { /* pass case */
                    Log.Comment( "Got correct exception: " + ae4.Message );
                    result = MFTestResults.Pass;
                }
                finally { File.Delete(file1Name); }
                try
                {
                    File.Create(file1Name).Close();
                    Log.Comment("Whitespace Destination Constructor");
                    File.Copy(file1.Name, "       ");
                    Log.Exception( "Expected ArgumentException" );
                    return MFTestResults.Fail;
                }
                catch(ArgumentException ae5)
                { /* pass case */
                    Log.Comment( "Got correct exception: " + ae5.Message );
                    result = MFTestResults.Pass;
                }
                finally { File.Delete(file1Name); }
            }
            catch (Exception ex)
            {
                Log.Exception("Unexpected exception: " + ex.Message);
                return MFTestResults.Fail;
            }

            return result;
        }

        [TestMethod]
        public MFTestResults IOExceptionTests()
        {
            MFTestResults result = MFTestResults.Pass;
            try
            {
                try
                {
                    File.Create(file1Name).Close();
                    Log.Comment("Relative Copy to '.'");
                    File.Copy(file1Name, ".");
                    Log.Exception( "Expected IOException" );
                    return MFTestResults.Fail;
                }
                catch (IOException ioe) 
                { 
                    /* pass case */  Log.Comment( "Got correct exception: " + ioe.Message );
                    result = MFTestResults.Pass;
                } /// We throw System.Exception with error code access_denied.
                finally { File.Delete(file1Name); }

                try
                {
                    File.Create(file1.FullName).Close();
                    Log.Comment("Absolute Copy to '.'");
                    File.Copy(file1.FullName, ".");
                    Log.Exception( "Expected IOException" );
                    return MFTestResults.Fail;
                }
                catch (IOException ioe) 
                { 
                    /* pass case */  Log.Comment( "Got correct exception: " + ioe.Message );
                    result = MFTestResults.Pass;
                }
                finally { File.Delete(file1.FullName); }

                try
                {
                    File.Create(file1Name).Close();
                    Log.Comment("Relative Copy to Directory");
                    File.Copy(file1Name, Path.Combine(IOTests.Volume.RootDirectory, sourceDir));
                    Log.Exception( "Expected IOException" );
                    return MFTestResults.Fail;
                }
                catch (IOException ioe) 
                { 
                    /* pass case */  Log.Comment( "Got correct exception: " + ioe.Message );
                    result = MFTestResults.Pass;
                }
                finally { File.Delete(file1Name); }

                try
                {
                    File.Create(file1.FullName).Close();
                    Log.Comment("Absolute Copy to Directory");
                    File.Copy(file1.FullName, Path.Combine(IOTests.Volume.RootDirectory, sourceDir));
                    Log.Exception( "Expected IOException" );
                    return MFTestResults.Fail;
                }
                catch (IOException ioe) 
                { 
                    /* pass case */  Log.Comment( "Got correct exception: " + ioe.Message );
                    result = MFTestResults.Pass;
                }
                finally { File.Delete(file1.FullName); }

                try
                {
                    File.Create(file1Name).Close();
                    Log.Comment("Relative Copy to self");
                    File.Copy(file1Name, file1Name);
                    Log.Exception( "Expected IOException" );
                    return MFTestResults.Fail;
                }
                catch (IOException ioe) 
                { 
                    /* pass case */  Log.Comment( "Got correct exception: " + ioe.Message );
                    result = MFTestResults.Pass;
                }
                finally { File.Delete(file1Name); }

                try
                {
                    File.Create(file1.FullName).Close();
                    Log.Comment("Absolute Copy to self");
                    File.Copy(file1.FullName, file1.FullName);
                    Log.Exception( "Expected IOException" );
                    return MFTestResults.Fail;
                }
                catch (IOException ioe) 
                {
                    /* pass case */
                    Log.Comment( "Got correct exception: " + ioe.Message ); 
                    result = MFTestResults.Pass;
                }
                finally { File.Delete(file1.FullName); }
            }
            catch (Exception ex)
            {
                Log.Exception("Unexpected exception: " + ex.Message);
                return MFTestResults.Fail;
            }

            return result;
        }

        [TestMethod]
        public MFTestResults ValidCases()
        {
            MFTestResults result = MFTestResults.Pass;
            string rootedSrcDir = Path.Combine(IOTests.Volume.RootDirectory, sourceDir);
            string rootedDstDir = Path.Combine(IOTests.Volume.RootDirectory, destDir);

            string file1Dir1 = Path.Combine(rootedSrcDir, file1Name);
            string file1Dir2 = Path.Combine(rootedDstDir, file1Name);
            string file2Dir1 = Path.Combine(rootedSrcDir, file2Name);
            string file2Dir2 = Path.Combine(rootedDstDir, file2Name);
            try
            {
                // relative copy
                if (!TestCopy(file1Name, file2Name))
                    return MFTestResults.Fail;

                // abosulte copy
                if (!TestCopy(file1Dir1, file2Dir1))
                    return MFTestResults.Fail;

                // copy dir1 to dir2
                if (!TestCopy(file2Dir1, file1Dir2))
                    return MFTestResults.Fail;

                // relative copy
                if (!TestCopy(file2Dir1, @"..\" + file1Name))
                    return MFTestResults.Fail;

                // copy file1dir1 to file2dir2
                if (!TestCopy(file2Dir1, file2Dir2))
                    return MFTestResults.Fail;

                if (result == MFTestResults.Pass)
                {
                    Log.Exception("Copy cases are now working!  Test needs to finish cases.");
                    result = MFTestResults.KnownFailure;
                }

            }
            catch (Exception ex)
            {
                Log.Exception("Unexpected exception: " + ex.Message);
                return MFTestResults.Fail;
            }

            return result;
        }

        #endregion Test Cases

        public MFTestMethod[] Tests
        {
            get
            {
                return new MFTestMethod[] 
                {
                    new MFTestMethod( ArgumentExceptionTests, "ArgumentExceptionTests" ),
                    new MFTestMethod( IOExceptionTests, "IOExceptionTests" ),
                    new MFTestMethod( ValidCases, "ValidCases" ),
                };
             }
        }
    }
}
