import React from 'react';
import {UserRow} from './UserRow';

export const UserList = ({ handlerUserSelectedForm, handlerRemoveUser, users }) => {
    return (
        <table className="table table-hover table-striped">
            <thead>
                <tr className='table-danger'>
                    <th>Tipo de Documento</th>
                    <th>Numero de Documento</th>
                    <th>Nombres</th>
                    <th>Sexo</th>
                    <th>Distrito</th>
                    <th>Provincia</th>
                    <th>Departamento</th>
                    <th></th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {users.map(({ idTrabajador, tipoDocumento, nroDocumento, nombres, sexo, nombreDistrito, nombreProvincia, nombreDepartamento,idDistrito }) => (
                    <UserRow
                        key={idTrabajador}
                        idTrabajador={idTrabajador}
                        tipoDocumento={tipoDocumento}
                        nroDocumento={nroDocumento}
                        nombres={nombres}
                        sexo={sexo}
                        nombreDistrito={nombreDistrito}
                        nombreProvincia={nombreProvincia}
                        nombreDepartamento={nombreDepartamento}
                        handlerUserSelectedForm={handlerUserSelectedForm}
                        handlerRemoveUser={handlerRemoveUser}
                        idDistrito={idDistrito} />
                ))
                }
            </tbody>
        </table>
    );
};
