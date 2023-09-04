import React, { useState } from 'react'
import Navbar from './Navbar'
import { useEffect } from 'react'
import axios from "axios"
import MuroPublicationStructure from './MuroPublicationStructure'
import FiltrosMuro from './FiltrosMuro'

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
        <div className='flex justify-center items-center mt-6 '>
             <div className='flex'>

             <div className='flex items-center justify-center h-screen mr-6'> 
                  <FiltrosMuro/>
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
