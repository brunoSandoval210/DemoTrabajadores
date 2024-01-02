import React from 'react';

export const UserRow = ({ handlerUserSelectedForm, handlerRemoveUser, idTrabajador, tipoDocumento, nroDocumento, nombres
    , sexo, nombreDistrito, nombreProvincia, nombreDepartamento,idDistrito }) => {
        const tipoSexo = sexo == 'M';
        const coloFila = tipoSexo ? 'table-primary' : 'table-warning';
    return (
        <tr key={idTrabajador} className={coloFila}>
            <td>{tipoDocumento}</td>
            <td>{nroDocumento}</td>
            <td>{nombres}</td>
            <td>{sexo}</td>
            <td>{nombreDistrito}</td>
            <td>{nombreProvincia}</td>
            <td>{nombreDepartamento}</td>
            <td>
                <button
                    type='button'
                    className='btn btn-secondary btn-sm'
                    onClick={() => handlerUserSelectedForm({
                        idTrabajador,
                        tipoDocumento,
                        nroDocumento,
                        nombres,
                        sexo,
                        idDistrito       
                    })}>
                    Editar
                </button>
            </td>
            <td>
                <button
                    type='button'
                    className='btn btn-danger btn-sm'
                    onClick={() => handlerRemoveUser(idTrabajador)}>
                    Eliminar
                </button>
            </td>
        </tr>
    );
};