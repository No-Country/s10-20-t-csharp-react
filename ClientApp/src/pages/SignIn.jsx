import React from 'react'
import { Link } from 'react-router-dom';
import GoogleIcon from '@mui/icons-material/Google';
import FacebookIcon from '@mui/icons-material/Facebook';
import axios from "axios"
import { useState, useEffect } from 'react';
import { useContext } from 'react';
import { UserContext } from '../store/userContext';
import imageFon from "../images/imageFon.png"
import imageFonTwo from "../images/img_myreports.png"

const SignIn = () => {

     const userCtx = useContext(UserContext)
        const [email, setEmail] = useState("")
        const [password, setPassword] = useState("")

       const loginAccount = () => { 
        const userToLog = ({ 
            email, 
            password
        })
        axios.post("https://127.0.0.1:44461/auth2/login", userToLog)
            .then((res) => { 
                console.log(res.data)
                userCtx.updateUser(res.data.data.id)
            })
            .catch((err) => { 
                console.log(err)
            })
        }
 

  return (
      <div>
          <>
            <div className='flex mt-12 gap-12 justify-center'>

                    <div className='text-center justify-center'>
                        <h1 className='font-bold text-xl'>Bienvenidos a QuejLAP </h1>
                         <div className=''>
                            <img src={imageFon} className='w-96'></img>
                            <img src={imageFonTwo} className='w-96'></img>
                         </div>
                    </div>

                <main className='flex justify-end  items-center  gap-10 sm:gap-0 my-6 mx-2 sm:mx-5 md:mx-10'>
                    <div className="basis-1/2 w-[50vh] flex flex-1 flex-col sm:gap-0 justify-center p-6 py-8 sm:py-6 lg:px-8 rounded-lg bg-gray-200 bg-opacity-60 shadow-md">
                        <h2 className="text-center text-2xl font-PoppinsBold leading-9 tracking-tight text-pallete-black"> Iniciar Sesion </h2>
                           
                                <div className="flex flex-col gap-6 sm:gap-4 mt-9 sm:mx-auto sm:w-full sm:max-w-sm">

                                    <div>
                                        <div className="mt-2">
                                            <input  id="Email" name="user" placeholder="Email" type="text" required className="input input-sm block w-full border border-black font-PoppinsRegular 
                                            ring-pallete-grey focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6" onChange={(e) => setEmail(e.target.value)}/>
                                        </div>
                                    </div>

                                    <div>
                                        <div className="mt-2">
                                            <input  id="Contraseña" name="Contraseña" placeholder="Contraseña" type="text" required className="input input-sm block w-full border border-black font-PoppinsRegular 
                                            ring-pallete-grey focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6"  onChange={(e) => setPassword(e.target.value)}
                                            />
                                            <p className="mt-2 text-xs text-black underline ml-[250px] cursor-pointer">Olvide la contraseña</p>
                                        </div>
                                    </div>

                                    <div className='justify-center text-center mt-6 bg-blue-950 border rounded-xl'>
                                        <button className=' bg-blue-950 border-none text-center  text-white' onClick={() => loginAccount()}>Iniciar Sesion</button>
                                    </div>

                                    <div className=' flex justify-center mt-4 font-bold'>
                                        <GoogleIcon className='mr-2'/>
                                        <button className='border-none text-center text-sm bg-white- border rounded-xl'>Iniciar Sesion con Google</button>
                                    </div>
                                
                                    <div className='justify-center text-center font-bold'>
                                            <FacebookIcon className='mr-2 '/>
                                            <button className='border-none  bg-white border text-sm rounded-xl'>Iniciar Sesion con Meta</button>
                                    </div>

                                    <div className='flex flex-col gap-3 mt-5 mx-auto items-center justify-center'>              
                                    <Link to={"/register"}><p className=" text-center underline text-xs sm:text-sm font-PoppinsSemibold text-pallete-grey">Registrarse con Email</p></Link> 
                                    </div>
                                </div>
                           
                    </div>
                </main>
           </div>
       </>
    </div>
  )
}

export default SignIn
