using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Web3Unity.Scripts.Library.ETHEREUEM.EIP;
using Web3Unity.Scripts.Library.Ethers.Providers;

public class CustomContract_Exmp : MonoBehaviour
{
                          ///////***************** this is a Example Script How to call Custom Contract ***************************\\\\\\\
    [SerializeField]
    private string erc20Contract = "0x5D3419B191A1BA371E715b72DDdCbec49dDe3505";
    [SerializeField]
    private string contractAddress = "0xC18cD43cC29a6Be85499C7DF23557591Bc17feD7";
    private string contractAbi = "[ { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" } ], \"name\": \"transferTokens\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"tokenAddress\", \"type\": \"address\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"constructor\" } ]";
   
    public string rPC = "https://rpc2-testnet.icbnetwork.info";
    [SerializeField]
    private string walletAddress;
    double amount;
    private void Start()
    {
        walletAddress = PlayerPrefs.GetString("Account");
        GetBalance20();
    }

    async private void GetBalance20()
    {
        var balanceOf = await ERC20.BalanceOf(erc20Contract, walletAddress);
        if (balanceOf != null)
        {
            BigInteger decimals = BigInteger.Pow(10, 18);
            float balanceERC20 = (float)balanceOf / (float)decimals;

            Debug.Log("balance: " + balanceERC20);
        }
    }
    // this function  is work how  to   swap,  coin to  erc20 token; 
    async public void SendTransaction()
    {
        Debug.Log("SendTranscation");
        BigInteger decimals = BigInteger.Pow(10, 18);
        BigInteger balanceERC20 = BigInteger.Divide(BigInteger.Multiply(new BigInteger(amount), decimals), BigInteger.One);
        string amount1 = balanceERC20.ToString();
        Debug.Log(amount1);
        string method = "transferTokens";
        string[] obj = { amount1 };
        string args = JsonConvert.SerializeObject(obj);
        string value = "0";
        string gasLimit = "";
        string gasPrice = "";

        try
        {
            string response = await Web3GL.SendContract(method, contractAbi, contractAddress, args, value, gasLimit, gasPrice);
            Debug.Log(response);


            //
            //    Check if transaction is successful or fail;
            var provider = new JsonRpcProvider(rPC);
            var Transaction = await provider.GetTransactionReceipt(response.ToString());

            while (Transaction.Status == null)
            {
                await new WaitForSeconds(1f);
                Debug.Log("Waiting For TX Status");
                var TransactionRecheck = await RPC.GetInstance.Provider().GetTransactionReceipt(response.ToString());
                Transaction = TransactionRecheck;
            }

            Debug.Log("Transaction Code: " + Transaction.Status);

            if (Transaction.Status.ToString() == "0")
            {
                Debug.Log("Transaction has failed");
            }
            else if (Transaction.Status.ToString() == "1")
            {

                Debug.Log("Transaction has been successful");
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }
   



     /// <summary>
     /// / How to list Nft  using chainsafe ;
     /// </summary>
    [Header("List NFt  Marketplace")]

    private string marketPlaceContractAbi = "[\r\n        {\r\n            \"inputs\": [],\r\n            \"stateMutability\": \"nonpayable\",\r\n            \"type\": \"constructor\"\r\n        },\r\n        {\r\n            \"anonymous\": false,\r\n            \"inputs\": [\r\n                {\r\n                    \"indexed\": false,\r\n                    \"internalType\": \"uint256\",\r\n                    \"name\": \"cutPerMillion\",\r\n                    \"type\": \"uint256\"\r\n                }\r\n            ],\r\n            \"name\": \"ChangedFeePerMillion\",\r\n            \"type\": \"event\"\r\n        },\r\n        {\r\n            \"anonymous\": false,\r\n            \"inputs\": [\r\n                {\r\n                    \"indexed\": false,\r\n                    \"internalType\": \"bytes32\",\r\n                    \"name\": \"orderId\",\r\n                    \"type\": \"bytes32\"\r\n                },\r\n                {\r\n                    \"indexed\": true,\r\n                    \"internalType\": \"address\",\r\n                    \"name\": \"seller\",\r\n                    \"type\": \"address\"\r\n                },\r\n                {\r\n                    \"indexed\": true,\r\n                    \"internalType\": \"address\",\r\n                    \"name\": \"tokenAddress\",\r\n                    \"type\": \"address\"\r\n                },\r\n                {\r\n                    \"indexed\": false,\r\n                    \"internalType\": \"uint256\",\r\n                    \"name\": \"tokenId\",\r\n                    \"type\": \"uint256\"\r\n                }\r\n            ],\r\n            \"name\": \"OrderCancelled\",\r\n            \"type\": \"event\"\r\n        },\r\n        {\r\n            \"anonymous\": false,\r\n            \"inputs\": [\r\n                {\r\n                    \"indexed\": false,\r\n                    \"internalType\": \"bytes32\",\r\n                    \"name\": \"orderId\",\r\n                    \"type\": \"bytes32\"\r\n                },\r\n                {\r\n                    \"indexed\": true,\r\n                    \"internalType\": \"address\",\r\n                    \"name\": \"seller\",\r\n                    \"type\": \"address\"\r\n                },\r\n                {\r\n                    \"indexed\": true,\r\n                    \"internalType\": \"address\",\r\n                    \"name\": \"tokenAddress\",\r\n                    \"type\": \"address\"\r\n                },\r\n                {\r\n                    \"indexed\": false,\r\n                    \"internalType\": \"uint256\",\r\n                    \"name\": \"tokenId\",\r\n                    \"type\": \"uint256\"\r\n                },\r\n                {\r\n                    \"indexed\": false,\r\n                    \"internalType\": \"uint256\",\r\n                    \"name\": \"askingPrice\",\r\n                    \"type\": \"uint256\"\r\n                },\r\n                {\r\n                    \"indexed\": false,\r\n                    \"internalType\": \"string\",\r\n                    \"name\": \"orderType\",\r\n                    \"type\": \"string\"\r\n                }\r\n            ],\r\n            \"name\": \"OrderCreated\",\r\n            \"type\": \"event\"\r\n        },\r\n        {\r\n            \"anonymous\": false,\r\n            \"inputs\": [\r\n                {\r\n                    \"indexed\": false,\r\n                    \"internalType\": \"bytes32\",\r\n                    \"name\": \"orderId\",\r\n                    \"type\": \"bytes32\"\r\n                },\r\n                {\r\n                    \"indexed\": true,\r\n                    \"internalType\": \"address\",\r\n                    \"name\": \"tokenAddress\",\r\n                    \"type\": \"address\"\r\n                },\r\n                {\r\n                    \"indexed\": false,\r\n                    \"internalType\": \"uint256\",\r\n                    \"name\": \"tokenId\",\r\n                    \"type\": \"uint256\"\r\n                },\r\n                {\r\n                    \"indexed\": true,\r\n                    \"internalType\": \"address\",\r\n                    \"name\": \"buyer\",\r\n                    \"type\": \"address\"\r\n                },\r\n                {\r\n                    \"indexed\": false,\r\n                    \"internalType\": \"uint256\",\r\n                    \"name\": \"sellPrice\",\r\n                    \"type\": \"uint256\"\r\n                },\r\n                {\r\n                    \"indexed\": false,\r\n                    \"internalType\": \"uint256\",\r\n                    \"name\": \"paidAmount\",\r\n                    \"type\": \"uint256\"\r\n                },\r\n                {\r\n                    \"indexed\": false,\r\n                    \"internalType\": \"uint256\",\r\n                    \"name\": \"saleShareAmount\",\r\n                    \"type\": \"uint256\"\r\n                }\r\n            ],\r\n            \"name\": \"OrderSuccessful\",\r\n            \"type\": \"event\"\r\n        },\r\n        {\r\n            \"anonymous\": false,\r\n            \"inputs\": [\r\n                {\r\n                    \"indexed\": false,\r\n                    \"internalType\": \"bytes32\",\r\n                    \"name\": \"orderId\",\r\n                    \"type\": \"bytes32\"\r\n                },\r\n                {\r\n                    \"indexed\": false,\r\n                    \"internalType\": \"uint256\",\r\n                    \"name\": \"expiryTime\",\r\n                    \"type\": \"uint256\"\r\n                }\r\n            ],\r\n            \"name\": \"OrderUpdated\",\r\n            \"type\": \"event\"\r\n        },\r\n        {\r\n            \"anonymous\": false,\r\n            \"inputs\": [\r\n                {\r\n                    \"indexed\": true,\r\n                    \"internalType\": \"address\",\r\n                    \"name\": \"previousOwner\",\r\n                    \"type\": \"address\"\r\n                },\r\n                {\r\n                    \"indexed\": true,\r\n                    \"internalType\": \"address\",\r\n                    \"name\": \"newOwner\",\r\n                    \"type\": \"address\"\r\n                }\r\n            ],\r\n            \"name\": \"OwnershipTransferred\",\r\n            \"type\": \"event\"\r\n        },\r\n        {\r\n            \"anonymous\": false,\r\n            \"inputs\": [],\r\n            \"name\": \"Pause\",\r\n            \"type\": \"event\"\r\n        },\r\n        {\r\n            \"anonymous\": false,\r\n            \"inputs\": [],\r\n            \"name\": \"Unpause\",\r\n            \"type\": \"event\"\r\n        },\r\n        {\r\n            \"inputs\": [],\r\n            \"name\": \"_INTERFACE_ID_ERC721\",\r\n            \"outputs\": [\r\n                {\r\n                    \"internalType\": \"bytes4\",\r\n                    \"name\": \"\",\r\n                    \"type\": \"bytes4\"\r\n                }\r\n            ],\r\n            \"stateMutability\": \"view\",\r\n            \"type\": \"function\"\r\n        },\r\n        {\r\n            \"inputs\": [\r\n                {\r\n                    \"internalType\": \"address\",\r\n                    \"name\": \"_tokenAddress\",\r\n                    \"type\": \"address\"\r\n                },\r\n                {\r\n                    \"internalType\": \"uint256\",\r\n                    \"name\": \"_tokenId\",\r\n                    \"type\": \"uint256\"\r\n                }\r\n            ],\r\n            \"name\": \"cancelOrder\",\r\n            \"outputs\": [],\r\n            \"stateMutability\": \"nonpayable\",\r\n            \"type\": \"function\"\r\n        },\r\n        {\r\n            \"inputs\": [\r\n                {\r\n                    \"internalType\": \"address\",\r\n                    \"name\": \"_tokenAddress\",\r\n                    \"type\": \"address\"\r\n                },\r\n                {\r\n                    \"internalType\": \"uint256\",\r\n                    \"name\": \"_tokenId\",\r\n                    \"type\": \"uint256\"\r\n                },\r\n                {\r\n                    \"internalType\": \"uint256\",\r\n                    \"name\": \"_askingPrice\",\r\n                    \"type\": \"uint256\"\r\n                },\r\n                {\r\n                    \"internalType\": \"string\",\r\n                    \"name\": \"_orderType\",\r\n                    \"type\": \"string\"\r\n                }\r\n            ],\r\n            \"name\": \"createOrder\",\r\n            \"outputs\": [],\r\n            \"stateMutability\": \"nonpayable\",\r\n            \"type\": \"function\"\r\n        },\r\n        {\r\n            \"inputs\": [],\r\n            \"name\": \"cutPerMillion\",\r\n            \"outputs\": [\r\n                {\r\n                    \"internalType\": \"uint256\",\r\n                    \"name\": \"\",\r\n                    \"type\": \"uint256\"\r\n                }\r\n            ],\r\n            \"stateMutability\": \"view\",\r\n            \"type\": \"function\"\r\n        },\r\n        {\r\n            \"inputs\": [],\r\n            \"name\": \"maxCutPerMillion\",\r\n            \"outputs\": [\r\n                {\r\n                    \"internalType\": \"uint256\",\r\n                    \"name\": \"\",\r\n                    \"type\": \"uint256\"\r\n                }\r\n            ],\r\n            \"stateMutability\": \"view\",\r\n            \"type\": \"function\"\r\n        },\r\n        {\r\n            \"inputs\": [\r\n                {\r\n                    \"internalType\": \"address\",\r\n                    \"name\": \"\",\r\n                    \"type\": \"address\"\r\n                },\r\n                {\r\n                    \"internalType\": \"address\",\r\n                    \"name\": \"\",\r\n                    \"type\": \"address\"\r\n                },\r\n                {\r\n                    \"internalType\": \"uint256\",\r\n                    \"name\": \"\",\r\n                    \"type\": \"uint256\"\r\n                },\r\n                {\r\n                    \"internalType\": \"bytes\",\r\n                    \"name\": \"\",\r\n                    \"type\": \"bytes\"\r\n                }\r\n            ],\r\n            \"name\": \"onERC721Received\",\r\n            \"outputs\": [\r\n                {\r\n                    \"internalType\": \"bytes4\",\r\n                    \"name\": \"\",\r\n                    \"type\": \"bytes4\"\r\n                }\r\n            ],\r\n            \"stateMutability\": \"nonpayable\",\r\n            \"type\": \"function\"\r\n        },\r\n        {\r\n            \"inputs\": [\r\n                {\r\n                    \"internalType\": \"address\",\r\n                    \"name\": \"\",\r\n                    \"type\": \"address\"\r\n                },\r\n                {\r\n                    \"internalType\": \"uint256\",\r\n                    \"name\": \"\",\r\n                    \"type\": \"uint256\"\r\n                }\r\n            ],\r\n            \"name\": \"orderByTokenId\",\r\n            \"outputs\": [\r\n                {\r\n                    \"internalType\": \"bytes32\",\r\n                    \"name\": \"orderId\",\r\n                    \"type\": \"bytes32\"\r\n                },\r\n                {\r\n                    \"internalType\": \"address payable\",\r\n                    \"name\": \"seller\",\r\n                    \"type\": \"address\"\r\n                },\r\n                {\r\n                    \"internalType\": \"uint256\",\r\n                    \"name\": \"askingPrice\",\r\n                    \"type\": \"uint256\"\r\n                },\r\n                {\r\n                    \"internalType\": \"address\",\r\n                    \"name\": \"tokenAddress\",\r\n                    \"type\": \"address\"\r\n                },\r\n                {\r\n                    \"internalType\": \"string\",\r\n                    \"name\": \"orderType\",\r\n                    \"type\": \"string\"\r\n                }\r\n            ],\r\n            \"stateMutability\": \"view\",\r\n            \"type\": \"function\"\r\n        },\r\n        {\r\n            \"inputs\": [],\r\n            \"name\": \"owner\",\r\n            \"outputs\": [\r\n                {\r\n                    \"internalType\": \"address\",\r\n                    \"name\": \"\",\r\n                    \"type\": \"address\"\r\n                }\r\n            ],\r\n            \"stateMutability\": \"view\",\r\n            \"type\": \"function\"\r\n        },\r\n        {\r\n            \"inputs\": [],\r\n            \"name\": \"pause\",\r\n            \"outputs\": [],\r\n            \"stateMutability\": \"nonpayable\",\r\n            \"type\": \"function\"\r\n        },\r\n        {\r\n            \"inputs\": [],\r\n            \"name\": \"paused\",\r\n            \"outputs\": [\r\n                {\r\n                    \"internalType\": \"bool\",\r\n                    \"name\": \"\",\r\n                    \"type\": \"bool\"\r\n                }\r\n            ],\r\n            \"stateMutability\": \"view\",\r\n            \"type\": \"function\"\r\n        },\r\n        {\r\n            \"inputs\": [],\r\n            \"name\": \"renounceOwnership\",\r\n            \"outputs\": [],\r\n            \"stateMutability\": \"nonpayable\",\r\n            \"type\": \"function\"\r\n        },\r\n        {\r\n            \"inputs\": [\r\n                {\r\n                    \"internalType\": \"address\",\r\n                    \"name\": \"_tokenAddress\",\r\n                    \"type\": \"address\"\r\n                },\r\n                {\r\n                    \"internalType\": \"uint256\",\r\n                    \"name\": \"_tokenId\",\r\n                    \"type\": \"uint256\"\r\n                }\r\n            ],\r\n            \"name\": \"safeExecuteOrder\",\r\n            \"outputs\": [],\r\n            \"stateMutability\": \"payable\",\r\n            \"type\": \"function\"\r\n        },\r\n        {\r\n            \"inputs\": [\r\n                {\r\n                    \"internalType\": \"uint256\",\r\n                    \"name\": \"_cutPerMillion\",\r\n                    \"type\": \"uint256\"\r\n                }\r\n            ],\r\n            \"name\": \"setOwnerCutPerMillion\",\r\n            \"outputs\": [],\r\n            \"stateMutability\": \"nonpayable\",\r\n            \"type\": \"function\"\r\n        },\r\n        {\r\n            \"inputs\": [\r\n                {\r\n                    \"internalType\": \"bool\",\r\n                    \"name\": \"_setPaused\",\r\n                    \"type\": \"bool\"\r\n                }\r\n            ],\r\n            \"name\": \"setPaused\",\r\n            \"outputs\": [],\r\n            \"stateMutability\": \"nonpayable\",\r\n            \"type\": \"function\"\r\n        },\r\n        {\r\n            \"inputs\": [\r\n                {\r\n                    \"internalType\": \"address\",\r\n                    \"name\": \"newOwner\",\r\n                    \"type\": \"address\"\r\n                }\r\n            ],\r\n            \"name\": \"transferOwnership\",\r\n            \"outputs\": [],\r\n            \"stateMutability\": \"nonpayable\",\r\n            \"type\": \"function\"\r\n        },\r\n        {\r\n            \"inputs\": [],\r\n            \"name\": \"unpause\",\r\n            \"outputs\": [],\r\n            \"stateMutability\": \"nonpayable\",\r\n            \"type\": \"function\"\r\n        }\r\n    ]";

    public string marketPlaceContractAddress = "0xfdbea2c2D77175C1E0508b874D40BEEAB0230Be2";

    private string Price;
    private int _tokenId;
    private string _tokenAddress;
    private string _orderType = "sell";
    async public void ListNFT()
        {
            
            float eth = float.Parse(Price);
            float decimals = 1000000000000000000; // 18 decimals
            float wei = eth * decimals;
            string _askingPrice = Convert.ToDecimal(wei).ToString();
            Debug.Log("price:" + _askingPrice);
            string method = "createOrder";
            string[] obj = {

               _tokenAddress,
               _tokenId.ToString(),
               _askingPrice,
                _orderType
        };
            string args = JsonConvert.SerializeObject(obj);
            string value = "0";
            string gasLimit = "";
            string gasPrice = "";
       
            string response = await Web3GL.SendContract(method, marketPlaceContractAbi, marketPlaceContractAddress, args, value, gasLimit, gasPrice);
            Debug.Log("***********  2");
            Debug.Log(response);
            // Check transction success or not 
            var provider = new JsonRpcProvider(rPC);
            var Transaction = await provider.GetTransactionReceipt(response.ToString());
            while (Transaction.Status == null)
            {
                await new WaitForSeconds(1f);
                Debug.Log("Waiting For TX Status");
                var TransactionRecheck =
                await RPC.GetInstance.Provider().GetTransactionReceipt(response.ToString());
                Transaction = TransactionRecheck;
            }
            Debug.Log("Transaction Code: " + Transaction.Status);
            if (Transaction.Status.ToString() == "0")
            {
                Debug.Log("Transaction has failed");
             
            }
            else if (Transaction.Status.ToString() == "1")
            {
                

                Debug.Log("Transaction has been successful");

            }
        }

    }

