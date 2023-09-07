import React from 'react'
import {Link} from "react-router-dom"
import axios from "axios"
import { useState, useEffect } from 'react'
import imageFon from "../images/imageFon.png"
import imageFonTwo from "../images/img_myreports.png"

const Register = () => {

   
     const [name, setName] = useState("")
     const [surname, setSurname] = useState("")
     const [birthdatePlace, setBirthdatePlace] = useState("")
     const [residence, setResidence] = useState("")
     const [birthdate, setBirthdate] = useState("")
     const [email, setEmail] = useState("")
     const [password, setPassword] = useState("")

  /*   const registerNewUser = () => { 
        const newUserToBeRegistered = ( { 
            name, 
            surname,
            birthdatePlace,
            residence,
            birthdate,
            email,
            password
        })
        axios.post("............", newUserToBeRegistered)
             .then((res) => { 
                console.log(res.data)
             })
             .catch((err) => { 
                console.log(err)
             })
     }*/
     


  return (
    <div>
        <>

        <div className='flex mt-12 gap-14 justify-center'>

            <div className='text-center justify-center'>
                <h1 className='font-bold text-xl'>Bienvenidos a QuejLAP </h1>
                <div>
                  <img src={imageFon} className='w-96'></img>
                  <img src={imageFonTwo} className='w-96'></img>
                </div>
               
            </div>
           <main className='flex justify-end  items-center  gap-10 sm:gap-0 my-6 mx-2 sm:mx-5 md:mx-10'>
            
                <div className="basis-1/2 w-[50vh] flex flex-1 flex-col sm:gap-0 justify-center p-6 py-8 sm:py-6 lg:px-8 rounded-lg bg-gray-200 bg-opacity-60 shadow-md">
                <h2 className="text-center text-2xl font-PoppinsBold leading-9 tracking-tight text-pallete-black">
                   Crear Cuenta
                </h2>

                <form className='w-full' action="#">
                    <div className="flex flex-col gap-6 sm:gap-4 mt-9 sm:mx-auto sm:w-full sm:max-w-sm">

                        <div>
                            <div className="mt-2">
                                <input  id="user" name="user" placeholder="Nombre" type="text" required className="input input-sm block w-full border border-black font-PoppinsRegular 
                                 focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6"  onChange={(e) => setName(e.target.value)}/>
                            </div>
                        </div>

                        <div>
                        <div className="mt-2">
                                <input  id="user" name="user" placeholder="Apellido" type="text" required className="input input-sm block w-full border border-black font-PoppinsRegular 
                                ring-pallete-grey focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6"  onChange={(e) => setSurname(e.target.value)}
                                />
                            </div>
                        </div>

                        <div>
                            <div className="mt-2">
                                <input   id="residence" name="residence" placeholder="Lugar de Nacimiento" type="text" required className="input input-sm block w-full border border-black font-PoppinsRegular 
                                ring-pallete-grey focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6"  onChange={(e) => setBirthdatePlace(e.target.value)}/>
                            </div>
                        </div>

                        <div>
                            <div className="mt-2">
                                <input  id="location"  name="location" placeholder="Localidad de Residencia" type="text"  autoComplete="location" required className="input input-sm block w-full border border-black font-PoppinsRegular 
                                ring-pallete-grey focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6"  onChange={(e) => setResidence(e.target.value)}/>
                            </div>
                        </div>

                    <div>
                        <div className="mt-2">
                            <input  id="date"  name="date" placeholder="Fecha de nacimiento" type="text" required className="input input-sm block w-full border border-black font-PoppinsRegular 
                                ring-pallete-grey focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6"  onChange={(e) => setBirthdate(e.target.value)}/>
                        </div>
                    </div>

                    <div className='mt-12'>
                        <div className="mt-2">
                            <input  id="email" name="email" placeholder="Email" type="Email" required  className="input input-sm block w-full border border-black font-PoppinsRegular 
                                ring-pallete-grey focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6"  onChange={(e) => setEmail(e.target.value)}/>
                        </div>
                    </div>

                    <div className="">
                        <div className="">
                        <input  id="password"  name="password"  placeholder="Contraseña" type="password" required className="input input-sm block w-full border border-black font-PoppinsRegular 
                                ring-pallete-grey focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6"  onChange={(e) =>setPassword(e.target.value)}/>
                           <p className="mt-2 text-sm text-gray-500">La contraseña debe contener al menos</p>
                           <ul>
                                <li className=" text-xs">. Una Mayúscula</li>
                                <li className=" text-xs">. Un número</li>
                                <li className=" text-xs">. 8 Caracteres</li>
                            </ul>
                        </div>
                     
                    </div>


                    <button  type="submit" className="btn btn-md bg-blue-950 text-white font-bold px-10 mt-4 border border-black ">
                        Registrarme
                    </button>

                    <div className='flex flex-col gap-3 mt-5 mx-auto r justify-end'>              
                      <Link to={"/login"}><p className=" text-center text-xs sm:text-sm font-PoppinsSemibold justify-end text-pallete-grey"> Ya tengo cuenta </p></Link>  
                    </div>
                    </div>
                </form>
                </div>
                </main>

        </div>

     
    </>
    </div>
  )
}

export default Register
