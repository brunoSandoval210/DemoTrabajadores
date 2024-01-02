import React from 'react';

export const FilterBySex = ({ applyFilter }) => {
  const onFiltroSexoChange = (e) => {
    applyFilter(e.target.value);
  };

  return (
    <div className="my-3">
      <label>Filtrar por Sexo:</label>
      <select
        className="form-control"
        onChange={onFiltroSexoChange}
      >
        <option value={null}>Todos</option>
        <option value="M">Masculino</option>
        <option value="F">Femenino</option>
      </select>
    </div>
  );
};