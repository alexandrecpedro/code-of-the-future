import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Estado } from '../models/estado';

import { firstValueFrom } from 'rxjs';
import { Municipio } from '../models/municipio';

export class IBGEServico{

    constructor(private http:HttpClient) { }

    public async listaEstados(): Promise<Estado[] | undefined> {
        let estados:Estado[] | undefined = await firstValueFrom(this.http.get<Estado[]>(`${environment.ibge}/estados`))
        return estados.sort((a, b) =>{
            return ('' + a.nome).localeCompare(b.nome.toString());
        });
    }
    public async listaMunicipiosPorEstado(id:Number): Promise<Municipio[] | undefined> {
        let municipios:Municipio[] | undefined = await firstValueFrom(this.http.get<Municipio[]>(`${environment.ibge}/estados/${id}/municipios`))

        return municipios.sort((a, b) =>{
            return ('' + a.nome).localeCompare(b.nome.toString());
        });
    }
}