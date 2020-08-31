using System.IO;

namespace Assets.Scripts.NFAudio
{
    public class Mp3Header
    {
        
        private ulong _bithdr;
        private bool _boolVBitRate;
        
        public int IntBitRate { get; private set; }
        public int IntFrequency { get; private set; }
        public int IntLength { get; private set; }
        private int _intVFrames;
        public long LngFileSize { get; private set; }
        public string StrFileName { get; private set; }
        public string StrLengthFormatted { get; private set; }
        public string StrMode { get; private set; }

        public bool ReadMp3Information(string fileName)
        {
            var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            
            StrFileName = @fs.Name;
            char[] chrSeparators = {'\\', '/'};
            var strSeparator = StrFileName.Split(chrSeparators);
            var intUpper = strSeparator.GetUpperBound(0);
            StrFileName = strSeparator[intUpper];

            StrFileName = StrFileName.Replace("'", "''");

     
            LngFileSize = fs.Length;

            var bytHeader = new byte[4];
            var bytVBitRate = new byte[12];
            var intPos = 0;

            do
            {
                fs.Position = intPos;
                fs.Read(bytHeader, 0, 4);
                intPos++;
                LoadMp3Header(bytHeader);
            } while(!IsValidHeader() && (fs.Position != fs.Length));

            if (fs.Position != fs.Length)
            {
                intPos += 3;

                if (GetVersionIndex() == 3) 
                {
                    if (GetModeIndex() == 3) 
                    {
                        intPos += 17;
                    }
                    else
                    {
                        intPos += 32;
                    }
                }
                else 
                {
                    if (GetModeIndex() == 3) 
                    {
                        intPos += 9;
                    }
                    else
                    {
                        intPos += 17;
                    }
                }

  
                fs.Position = intPos;
                fs.Read(bytVBitRate, 0, 12);
                _boolVBitRate = LoadVbrHeader(bytVBitRate);
               
                IntBitRate = GetBitrate();
                IntFrequency = GetFrequency();
                StrMode = GetMode();
                IntLength = GetLengthInSeconds();
                StrLengthFormatted = GetFormattedLength();
                fs.Close();
                return true;
            }
            return false;
        }

        private void LoadMp3Header(byte[] c)
        {

            _bithdr = (ulong)(((c[0] & 255) << 24) | ((c[1] & 255) << 16) | ((c[2] & 255) << 8) | ((c[3] & 255)));
        }

        private bool LoadVbrHeader(byte[] inputheader)
        {
 
            if (inputheader[0] == 88 && inputheader[1] == 105 &&
                inputheader[2] == 110 && inputheader[3] == 103)
            {
                var flags =
                    ((inputheader[4] & 255) << 24) | ((inputheader[5] & 255) << 16) | ((inputheader[6] & 255) << 8) | ((inputheader[7] & 255));
                if ((flags & 0x0001) == 1)
                {
                    _intVFrames =
                        ((inputheader[8] & 255) << 24) | ((inputheader[9] & 255) << 16) | ((inputheader[10] & 255) << 8) |
                        ((inputheader[11] & 255));
                    return true;
                }
                _intVFrames = -1;
                return true;
            }
            return false;
        }

        private bool IsValidHeader()
        {
            return (((GetFrameSync() & 2047) == 2047) &&
                    ((GetVersionIndex() & 3) != 1) &&
                    ((GetLayerIndex() & 3) != 0) &&
                    ((GetBitrateIndex() & 15) != 0) &&
                    ((GetBitrateIndex() & 15) != 15) &&
                    ((GetFrequencyIndex() & 3) != 3) &&
                    ((GetEmphasisIndex() & 3) != 2));
        }

        private int GetFrameSync()
        {
            return (int)((_bithdr >> 21) & 2047);
        }

        private int GetVersionIndex()
        {
            return (int)((_bithdr >> 19) & 3);
        }

        private int GetLayerIndex()
        {
            return (int)((_bithdr >> 17) & 3);
        }

        private int GetProtectionBit()
        {
            return (int)((_bithdr >> 16) & 1);
        }

        private int GetBitrateIndex()
        {
            return (int)((_bithdr >> 12) & 15);
        }

        private int GetFrequencyIndex()
        {
            return (int)((_bithdr >> 10) & 3);
        }

        private int GetPaddingBit()
        {
            return (int)((_bithdr >> 9) & 1);
        }

        private int GetPrivateBit()
        {
            return (int)((_bithdr >> 8) & 1);
        }

        private int GetModeIndex()
        {
            return (int)((_bithdr >> 6) & 3);
        }

        private int GetModeExtIndex()
        {
            return (int)((_bithdr >> 4) & 3);
        }

        private int GetCoprightBit()
        {
            return (int)((_bithdr >> 3) & 1);
        }

        private int GetOrginalBit()
        {
            return (int)((_bithdr >> 2) & 1);
        }

        private int GetEmphasisIndex()
        {
            return (int)(_bithdr & 3);
        }

        private double GetVersion()
        {
            double[] table = {2.5, 0.0, 2.0, 1.0};
            return table[GetVersionIndex()];
        }

        private int GetLayer()
        {
            return 4 - GetLayerIndex();
        }

        private int GetBitrate()
        {
 
            if (_boolVBitRate)
            {
                var medFrameSize = LngFileSize/(double)GetNumberOfFrames();
                return (int)((medFrameSize*GetFrequency())/(1000.0*((GetLayerIndex() == 3) ? 12.0 : 144.0)));
            }
            int[,,] table =
            {
                {
                    
                    {0, 8, 16, 24, 32, 40, 48, 56, 64, 80, 96, 112, 128, 144, 160, 0}, 
                    {0, 8, 16, 24, 32, 40, 48, 56, 64, 80, 96, 112, 128, 144, 160, 0}, 
                    {0, 32, 48, 56, 64, 80, 96, 112, 128, 144, 160, 176, 192, 224, 256, 0} 
                },
                {
                    
                    {0, 32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320, 0}, 
                    {0, 32, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320, 384, 0}, 
                    {0, 32, 64, 96, 128, 160, 192, 224, 256, 288, 320, 352, 384, 416, 448, 0} 
                }
            };

            return table[GetVersionIndex() & 1, GetLayerIndex() - 1, GetBitrateIndex()];
        }

        private int GetFrequency()
        {
            int[,] table =
            {
                {32000, 16000, 8000}, 
                {0, 0, 0}, 
                {22050, 24000, 16000}, 
                {44100, 48000, 32000} 
            };

            return table[GetVersionIndex(), GetFrequencyIndex()];
        }

        private string GetMode()
        {
            switch(GetModeIndex())
            {
                default:
                    return "Stereo";
                case 1:
                    return "Joint Stereo";
                case 2:
                    return "Dual Channel";
                case 3:
                    return "Single Channel";
            }
        }

        private int GetLengthInSeconds()
        {
            
            var intKiloBitFileSize = (int)((8*LngFileSize)/1000);
            return intKiloBitFileSize/GetBitrate();
        }

        private string GetFormattedLength()
        {
            
            var s = GetLengthInSeconds();

            
            var ss = s%60;

            
            var m = (s - ss)/60;

            
            var mm = m%60;

            
            var h = (m - mm)/60;

            
            return h.ToString("D2") + ":" + mm.ToString("D2") + ":" + ss.ToString("D2");
        }

        private int GetNumberOfFrames()
        {
            
            if (!_boolVBitRate)
            {
                var medFrameSize = ((GetLayerIndex() == 3) ? 12 : 144)*((1000.0*GetBitrate())/GetFrequency());
                return (int)(LngFileSize/medFrameSize);
            }
            return _intVFrames;
        }
    }
}