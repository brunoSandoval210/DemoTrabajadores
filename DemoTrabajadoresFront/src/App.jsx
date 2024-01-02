import React, { useState, useEffect, useReducer } from 'react';
import axios from 'axios';
import { UserForm } from './components/UserForm'
import { UserList } from './components/UserList'
import Swal from 'sweetalert2';
import { FilterBySex } from './components/FilterBySex';

export const App = () => {
  const [users, setUsers] = useState([]);
  const [visibleForm, setVisibleForm] = useState(false);
  const initialUserForm = {
    idTrabajador: '',
    tipoDocumento: '',
    nroDocumento: '',
    nombres: '',
    sexo: '',
    idDistrito: '',
  }
  const [userSelected, setUserSelected] = useState(initialUserForm);
  useEffect(() => {
    initialUsers();
  }, []);
  const applyFilter =  (sexFilter) => {
    initialUsers(sexFilter);
  };
  const initialUsers = async (sexFilter) => {
    try {
      let url;
  
      if (sexFilter === 'M' || sexFilter === 'F') {
        url = `https://localhost:7150/trabajadores/sexo/${sexFilter}`;
      } else {
        url = "https://localhost:7150/trabajadores/detalle";
      }
  
      const resultado = await axios.get(url);

      setUsers(resultado.data);
    } catch (error) {
      console.error('Error al cargar la lista de trabajadores', error);
    }
  };
  
  

  const handlerRemoveUser = async (id) => {
    try {
      Swal.fire({
        title: '¿Está seguro de que desea eliminar?',
        text: '¡Cuidado, el trabajador será eliminado!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, eliminar'
      }).then(async (result) => {
        if (result.isConfirmed) {
          await axios.delete(`https://localhost:7150/trabajadores/${id}`);
          Swal.fire(
            'Trabajador Eliminado',
            'El trabajador ha sido eliminado con éxito',
            'success'
          );
          initialUsers();
        }
      });
    } catch (error) {
      console.error(error);
    }
  };


  const handlerAddUser = async (user) => {
    try {
      if (user.idTrabajador == 0) {
        await axios.post('https://localhost:7150/trabajadores', user);
      } else {
        await axios.put(`https://localhost:7150/trabajadores/${user.idTrabajador}`, user);
      }
      Swal.fire(
        (user.idTrabajador == 0) ?
          'Trabajador Creado' :
          'Trabajador Actualizado',
        (user.idTrabajador == 0) ?
          'El trabajador ha sido creado con exito!' :
          'El trabajador ha sido actualizado con exito!',
        'success'
      );
      initialUsers();
      handlerCloseForm();
    } catch (error) {
      console.error(error);
    }
  };


  const handlerUserSelectedForm = async (user) => {
    console.log(user);
    setUserSelected({ ...user })
    setVisibleForm(true);
  };

  const handlerOpenForm = () => {
    setVisibleForm(true);
  }

  const handlerCloseForm = () => {
    setVisibleForm(false);
    setUserSelected(initialUserForm);
  }
 
  return (
    <>

      <div className='container my-4'>
        <div className='row'>
          {!visibleForm ||
            <div className="abrir-modal animacion fadeIn">
              <div className="modal " style={{ display: "block" }} tabIndex="-1">
                <div className="modal-dialog" role="document">
                  <div className="modal-content">
                    <div className="modal-header">
                      <h5 className="modal-title">
                        {userSelected.id > 0 ? 'Editar' : 'Crear'} Modal Usuarios
                      </h5>
                    </div>
                    <div className="modal-body">
                      <UserForm
                        initialUserForm={initialUserForm}
                        userSelected={userSelected}
                        handlerAddUser={handlerAddUser}
                        handlerCloseForm={handlerCloseForm}
                      />
                    </div>
                  </div>
                </div>
              </div>
            </div>
          }

          <div className='col'>
            {visibleForm ||
              <button
                className='btn btn-primary my-2'
                onClick={handlerOpenForm}
              >
                Nuevo Usuario
              </button>}
              <FilterBySex applyFilter={applyFilter} />
            {users.length === 0
              ? <div className="alert alert-info" role="alert">
                No hay registros
              </div>
              : <UserList users={users}
                handlerRemoveUser={handlerRemoveUser}
                handlerUserSelectedForm={handlerUserSelectedForm} />}
                
          </div>

        </div>
      </div>
    </>
  )
}

