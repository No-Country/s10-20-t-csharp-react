import React, { useState } from 'react'
import Navbar from './Navbar'
import { useEffect } from 'react'
import axios from "axios"

const Muro = () => {

  const [allPublications, setAllPublications] = useState([])


    useEffect(() => { 
      axios.get("https://s10nc.somee.com/api/Quejas?SortColumn=CreatedAt&SortOrder=DESC&PageIndex=1&PageSize=10")
          .then((res) => { 
            console.log(res.data.data)
            setAllPublications(res.data.data)
          })
          .catch((err) => { 
            console.log(err)
          })
    }, [])

  return (
    <div> 
        <Navbar/>

                

        <div className='flex justify-center items-center mt-12 '>
             <div className='flex'>
                 <div className='mr-48'>
                  <input type="text" placeholder='Buscar' />
                       <div className='mt-6'>
                            <div className='flex mt-4'>
                            <input type="checkbox" className="checkbox checkbox-sm" /> 
                            <p>Aceras</p>
                            </div>

                            <div className='flex mt-4'>
                            <input type="checkbox" className="checkbox checkbox-sm" /> 
                               <p>Limpieza</p>
                            </div>

                            <div className='flex mt-4'>
                            <input type="checkbox" className="checkbox checkbox-sm" /> 
                            <p>Luminarias</p>
                            </div>

                            <div className='flex mt-4'>
                               <input type="checkbox" className="checkbox checkbox-sm" /> 
                               <p>Parques y Plazas</p>
                            </div> 

                            <div className='flex mt-4'>
                               <input type="checkbox" className="checkbox checkbox-sm" /> 
                               <p>Transito</p>
                            </div> 

                            <div className='flex mt-4'>
                               <input type="checkbox" className="checkbox checkbox-sm" /> 
                               <p>Todo</p>
                            </div> 

                            <div className='mt-4'>
                              <p className='cursor-pointer text-sm'>Eliminar todos los filtros</p>
                            </div>
                       </div>
                 </div>

                 <div >
                  <div className='flex justify-center items-center'>
                   <div className="dropdown">
                      <label tabIndex={0} className="btn m-1">Ordenar Por</label>
                        <ul tabIndex={0} className="dropdown-content z-[1] menu p-2 shadow bg-base-100 rounded-box w-52">
                            <li><a>Mas Recientes</a></li>
                            <li><a>Mas Antiguos</a></li>
                        </ul>
                    </div>
              </div> 


              {allPublications.map((p) => ( 
                    <div className="">
                                <div className="card w-96 bg-base-100 shadow-xl mt-2 border rounded-xl">
                                    <div className='flex'>
                                       <div className="avatar">
                                          <div className="w-8 rounded-full">
                                              <img src="data:image/jpeg;base64,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" />
                                           </div>
                                       </div>

                                       <div className='flex justify-between '>
                                          <p className="text-gray-400 ml-6 ">{p.userName}</p>
                                          <p className=''>Categoria</p>
                                       </div>
                                    
                                    </div>
                                    <div className=' ml-4'>
                                        <p className='font-bold text-sm color-black'>{p.title}</p>
                                        <p className='justify-center  text-xs mr-4'>{p.text}</p>

                                        <div className='mt-2 '>
                                           <p className=' text-xs mr-4'>{p.district_Name}</p>
                                           <p className=' text-xs mr-4 underline cursor-pointer'>Ver en Mapa</p>
                                        </div>
                                    </div>
                                    <div className='flex mt-4 mb-4'>
                                         <div className="avatar">
                                            <div className="w-24 rounded">
                                                <img src="https://www.elcivismo.com.ar/imagenes/fotos/32495.jpg" />
                                            </div>
                                         </div>

                                         <div className="avatar">
                                            <div className="w-24 rounded ml-4">
                                                <img src="https://cdn1.eldia.com/112018/1542172077037.jpg?&cw=630" />
                                            </div>
                                         </div>
                                    </div>                                                                  
                               </div>
                        </div>  
              ))}
              
                                         
                 </div>

                 
           
             </div>
        </div>
    </div>
  )
}

export default Muro
