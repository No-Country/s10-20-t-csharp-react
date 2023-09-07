import React from 'react'
import SearchIcon from '@mui/icons-material/Search';
import axios from 'axios';
import { useState } from 'react';
import { useEffect } from 'react';
import {useNavigate} from "react-router-dom"



const FiltrosMuro = () => {

        const [searchParam, setSearchParam] = useState("")
        const [searchResults, setSearchResults] = useState([])
        const navigate = useNavigate()

      /*  const searchParamPublication = () => { 
          axios.get(`http://localhost:4000/getPublicationsWithParams/${searchParam}`)
              .then((res) => { 
                console.log(res.data)
                setSearchResults(res.data)
                setTimeout(() => { 
                  navigate(`/publicationsSearched/${searchParam}`)
                },200)
                setTimeout(() => { 
                 window.location.reload()
                }, 500)
              })
              .catch((err) => { 
                console.log(err)
              })
        }*/

  return (
    <div>
        <div className='mr-4'>
                        <div className='flex'>
                          <input type="text" placeholder='Search' className='bg-gray-200 border border-gray-200 rounded-lg text-center ' onChange={(e) => setSearchParam(e.target.value)}/>
                          <div >
                             < SearchIcon style={{cursor: "pointer", marginLeft:"3px"}}/>
                          </div>                       
                        </div>
                 
                       <div className='mt-6'>
                            <div className='flex mt-4'>
                              <input type="checkbox" className="checkbox checkbox-sm" /> 
                              <p className='ml-2'>Aceras</p>
                            </div>

                            <div className='flex mt-4'>
                              <input type="checkbox" className="checkbox checkbox-sm" /> 
                              <p className='ml-2'>Luminarias</p>
                             </div>

                            <div className='flex mt-4'>
                              <input type="checkbox" className="checkbox checkbox-sm" /> 
                              <p className='ml-2'>Limpieza</p>
                            </div>

                            <div className='flex mt-4'>
                               <input type="checkbox" className="checkbox checkbox-sm" /> 
                               <p className='ml-2'>Parques y Plazas</p>
                            </div> 

                            <div className='flex mt-4'>
                               <input type="checkbox" className="checkbox checkbox-sm" /> 
                               <p className='ml-2'>Transito</p>
                            </div> 

                            <div className='flex mt-4'>
                               <input type="checkbox" className="checkbox checkbox-sm" /> 
                               <p className='ml-2'>Todo</p>
                            </div> 

                            <div className='flex mt-4'>
                               <p>Eliminar todos</p>
                            </div> 
                       </div>
                 </div>
    </div>
  )
}

export default FiltrosMuro
