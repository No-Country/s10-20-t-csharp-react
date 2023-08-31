import React, { useState } from 'react'
import Navbar from './Navbar'
import { useEffect } from 'react'
import axios from "axios"
import MuroPublicationStructure from './MuroPublicationStructure'

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
                  <input type="text" className='border-none hover:border-none rounded-lg bg-gray-50 text-black' placeholder='Buscar' />
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

              <div className=''>
                          {allPublications.map((p) => (
                            <div className='p-6 ' key={p.id}>
                              <MuroPublicationStructure pub={p} />
                            </div>
                          ))}
                    </div>                            
                 </div>
             </div>
        </div>
    </div>
  )
}

export default Muro
