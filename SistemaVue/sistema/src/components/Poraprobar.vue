<template>
    <v-layout align-start>
        <v-flex>
            <v-container v-if="verOrdenpago==0">
                <v-toolbar flat color="white">
                    <v-toolbar-title>Orden de pago por aprobar</v-toolbar-title>
                    <v-divider
                    class="mx-2"
                    inset
                    vertical
                    ></v-divider>
                    <v-spacer></v-spacer>
                    <v-text-field class="text-xs-center" v-model="search" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
                    <v-spacer></v-spacer> 
                </v-toolbar>
                <v-data-table
                    :headers="headers"
                    :items="ordendepagos"
                    :search="search"
                    :items-per-page="15"
                    class="elevation-1">                   
                    <template v-slot:[`item.opciones`]="{ item }">
                        <td class=" justify-center layout px-0 mt-3">
                            <v-btn color="primary" @click="editItem(item)">Ver</v-btn>
                        </td>    
                    </template>
                    <template slot="items" slot-scope="props">
                        <td>{{ props.item.idordendepago }}</td>
                        <td>{{ props.item.estado }}</td>
                        <td>{{ props.item.usuario }}</td>
                        <td>{{ props.item.regional }}</td>  
                        <td>{{ props.item.cuenta}}</td>                 
                    </template>
                    <template v-slot:[`item.rendido`]="{ item }">
                    <div v-if="item.rendido==1">
                        <span class="blue--text">Si</span>
                    </div>
                    <div v-if="item.rendido==0">
                        <span class="red--text">No</span>
                    </div>
                </template>
                    <template slot="no-data">
                        <v-btn color="primary" @click="listar">No hay datos</v-btn>
                    </template>
                </v-data-table>
            </v-container>
            <v-container v-if="verOrdenpago">
                <template fluid>
                    <v-btn color="primary" @click="volver">Volver</v-btn>
                    <p class="text-subtitle-2 text-center">CASO: {{id}}</p>     
                    <v-row aling="center">
                        <v-toolbar color="blue" dark>Datos Usuario</v-toolbar>     
                    </v-row>
                    <v-row aling="center" cols="6" sm="1">
                        <v-col class="d-inline-flex" >
                            <v-subheader>Usuario:</v-subheader>
                            <v-text-field v-model="nombreusuario" placeholder="Placeholder" outlined disabled ></v-text-field>
                        </v-col>
                        <v-col class="d-flex">
                            <v-subheader>Area:</v-subheader>
                            <v-select  v-model="idarea" :items="Areas" label="Selecione un area" outlined></v-select>
                        </v-col>
                        <v-col class="d-flex">
                            <v-subheader>Regional:</v-subheader>
                            <v-select  v-model="idregional" :items="Regionales" label="Selecione una regional" outlined></v-select>
                        </v-col>
                    </v-row>
                    <v-row aling="center" cols="6" sm="1">
                        <v-col class="d-flex">
                            <v-subheader>Empresa:</v-subheader>
                            <v-select  v-model="idempresa" :items="Empresas" label="Seleccione una empresa" outlined></v-select>
                        </v-col>
                        <v-col class="d-flex">
                            <v-subheader>fecha solicitud:</v-subheader>
                            <v-text-field v-model="fechasolicitada" class="text-xs-center" disabled></v-text-field>
                        </v-col>
                        <v-col class="d-flex">
                            <v-subheader>fechaprogramada:</v-subheader>
                            <v-menu v-model="menu2" :close-on-content-click="false" :nudge-right="40"
                            transition="scale-transition" offset-y min-width="auto">
                                <template v-slot:activator="{ on, attrs }">
                                    <v-text-field v-model="fechaprogramada"  prepend-icon="mdi-calendar"
                                        readonly v-bind="attrs" v-on="on">
                                    </v-text-field>
                                </template>
                                <v-date-picker v-model="fechaprogramada" @input="menu2 = false">                                
                                </v-date-picker>
                            </v-menu>
                        </v-col>
                    </v-row>
                    <v-row aling="center" cols="6" sm="2">
                        <v-col class="d-flex">
                            <v-subheader>Tipo de Gasto:</v-subheader>
                            <v-select  v-model="idtipogasto" :items="TipoGastos" label="Seleccione tipo de gasto" @change="Selectdinamic(idtipogasto)" outlined ></v-select>
                        </v-col>
                        <v-col class="d-flex">
                            <v-subheader>Especificaion de gasto:</v-subheader>
                            <v-select  v-model="idespecifgasto" :items="EspecifGastos" label="Seleccione especificacion" outlined></v-select>
                        </v-col>
                    </v-row>
                    <v-row aling="center">
                        <v-toolbar color="blue" dark>Datos solicitud</v-toolbar>     
                    </v-row>
                    <v-row aling="center" >
                        <v-col class="d-flex" >
                            <v-subheader>Proyecto:</v-subheader>
                            <v-select  v-model="idproyecto" :items="Proyectos" label="Seleccione proyecto" outlined></v-select>
                        </v-col> 
                        <v-divider inset vertical></v-divider> 
                        <v-col class="d-flex" >
                            <v-subheader>Moneda:</v-subheader>
                            <v-select  v-model="idmoneda" :items="Monedas" label="Seleccione moneda" outlined></v-select>
                        </v-col>  
                        <v-divider inset vertical></v-divider>
                        <v-col class="d-flex">
                            <v-subheader>modo pago:</v-subheader>
                            <v-select  v-model="idmodopago" :items="ModoPagos" label="Seleccione modo pago" outlined></v-select>
                        </v-col>      
                    </v-row>
                    <v-row aling="center">
                        <v-col class="d-flex" cols="6" sm="4">
                            <v-subheader>tipo de solicitud:</v-subheader>
                            <v-select  v-model="idtiposolicitud" :items="TipoSolicitudes" label="Seleccione tipo de solicitud" outlined></v-select>
                        </v-col> 
                        <v-divider inset vertical></v-divider> 
                        <v-col class="d-flex" cols="6" sm="3">
                            <label class="mt-5 ml-4">Tiene:</label>
                            <v-spacer></v-spacer>
                            <v-checkbox v-model="factura" color="blue" label="Factura" row></v-checkbox> 
                            <v-spacer></v-spacer>
                            <v-checkbox v-model="recibo" color="blue" label="Recibo"></v-checkbox>   
                        </v-col>
                    </v-row>
                    <v-row aling="center">
                        <template >
                            <v-toolbar dark color="blue" class="mt-2">
                                <v-toolbar-title>Cuenta destino:</v-toolbar-title>
                                <v-autocomplete v-model="idcuenta" :items="Cuentas" 
                                    :search-input.sync="searchcuentas" cache-items
                                    class="mx-6" hide-details label="Escriba el nro de cuento o el nombre" solo-inverted>
                                </v-autocomplete>
                            </v-toolbar>   
                        </template>    
                    </v-row>
                    <v-row aling="center">
                        <v-textarea v-model="concepto" label="Concepto" auto-grow outlined
                            rows="3" row-height="25" class="mt-4"></v-textarea> 
                    </v-row>
                    <v-container>
                        <v-btn color="blue darken-1" @click="agregarDetalle">+</v-btn>
                        <v-data-table
                            :headers="cabeceraDetalles"
                            :items="detalles"
                            hide-default-footer
                            class="elevation-2">
                            <template v-slot:[`item.borrar`]="{ item }">
                                <td class=" justify-center layout px-0">
                                    <v-icon small class="mr-2" @click="eliminardetalle(detalles,item)">
                                        delete
                                    </v-icon>
                                </td>
                            </template>
                            <template v-slot:[`item.detalle`]="props">
                                <td><v-text-field v-model="props.item.detalle"></v-text-field></td>
                            </template>
                            <template v-slot:[`item.nrodocumento`]="props">
                                <td><v-text-field v-model="props.item.nrodocumento"></v-text-field></td>
                            </template>
                            <template v-slot:[`item.monto`]="props">
                                <td><v-text-field type="Number" v-model="props.item.monto"></v-text-field></td>
                            </template>
                            <template slot="no-data" black color="red">
                                <strong>DEBE AGREGAR EL DETALLE</strong>
                            </template>
                        </v-data-table>
                        <v-flex class="text-right mt-2 mr-10">
                            <strong>Total:</strong>{{total=(calcularTotal).toFixed(2)}}
                        </v-flex>
                    </v-container>
                </template>
                <template>
                    <v-container class="mt-5">
                        <v-row aling="center" cols="6" sm="1">
                            <v-spacer></v-spacer>
                            <v-subheader>Contabilidad:</v-subheader>
                            <v-select class="mr-10" v-model="idcontador" :items="Contadores" label="Selecione un encargado de contabilidad" outlined></v-select>
                        </v-row>
                        <v-row aling="center" cols="6" sm="2">
                            <v-spacer></v-spacer>
                            <v-btn class="mt-2 mr-10" color="green darken-1" @click="cambiarAprobador" >Cambiar aprobador</v-btn>
                            <v-subheader>Aprobador:</v-subheader>
                            <v-select class="mr-10" v-model="idaprobador" :items="Aprobadores" label="Selecione un aprobador" outlined></v-select>
                        </v-row>
                        <v-flex xs12 sm12 md12 v-show="valida">
                            <div class="red--text" v-for="v in validaMensaje" :key="v" v-text="v">
                             </div>
                        </v-flex>
                        <v-row>
                            <v-spacer></v-spacer>
                            <v-btn class="mt-2 mr-10" color="blue darken-1" @click="aprobar(1)" >Aprobar</v-btn>
                            <v-btn class="mt-2 mr-10" color="red darken-1" @click="aprobar(0)" >Rechazar</v-btn>
                            <v-btn class="mt-2" color="primary" @click="volver">Volver</v-btn>
                            <v-spacer></v-spacer>
                        </v-row >
                        <v-dialog v-model="dialog" max-width="500px">
                            <v-card>
                                <v-card-title>
                                <span class="headline">{{ formTitle}}</span>
                                </v-card-title>
                                <v-card-text>hola</v-card-text>
                                <v-card-actions>
                                    <v-spacer></v-spacer>
                                    <v-btn @click.native="close" rounded block elevation="10" color="blue" dark class="mb-3">Cerrar</v-btn>
                                    <v-spacer></v-spacer>
                                </v-card-actions>
                            </v-card>
                        </v-dialog>
                        <v-dialog v-model="dialogAprobador" max-width="500px">
                            <v-card>
                                <v-card-title>
                                <span class="headline">Se ha modificado el aprobador</span>
                                </v-card-title>
                                <v-card-text>
                                hola
                                </v-card-text>
                                <v-card-actions>
                                    <v-spacer></v-spacer>
                                    <v-btn  @click.native="close" rounded block elevation="10" color="blue" dark class="mb-3">Cerrar</v-btn>
                                    <v-spacer></v-spacer>
                                </v-card-actions>
                            </v-card>
                        </v-dialog>                  
                    </v-container>
                </template>
                <v-flex xs12 sm12 md12 v-show="valida">
                    <div class="red--text" v-for="v in validaMensaje" :key="v" v-text="v">
                    </div>
                </v-flex>
            </v-container>
        </v-flex>
    </v-layout>
</template>

<script>
    import axios from 'axios'
    export default {
        data(){
            return {
                ordendepagos: [],
                dialog: false,
                dialogAprobador:false,
                headers: [                    
                    { text: 'CASO', value: 'idordendepago'},
                    { text: 'Opciones', value: 'opciones'  } ,
                    { text: 'ESTADO', value: 'estado'},
                    { text: 'USUARIO', value: 'usuario'  },
                    { text: 'EMPRESA', value: 'empresa'  },
                    //{ text: 'REGIONAL', value: 'regional'  },
                    //{ text: 'AREA', value: 'area'  },
                    { text: 'ESPECIFICACION', value: 'especifgasto' },
                    { text: 'APROBADOR', value: 'aprobador' },
                    //{ text: 'TIPO DE PAGO', value: 'tiposolicitud' },
                    { text: 'FECHA SOLICITADO', value: 'fechasolicitud' },
                    { text: 'FECHA PROGRAMADA', value: 'fechaprogramada' },
                    { text: 'CUENTA', value: 'cuenta' },                   
                    { text: 'MONEDA', value: 'moneda' },
                    { text: 'IMPORTE', value: 'total' },
                    { text: 'RENDICION', value: 'rendido' },
                    //{ text: 'CONCEPTO', value: 'concepto' }
                    ],
                detalles:[],
                cabeceraDetalles: [
                    { text: 'Borrar', value: 'borrar', sortable: false } ,
                    { text: 'Detalle', value: 'detalle' },
                    { text: 'Nro Documento', value: 'nrodocumento' },
                    { text: 'Monto', value: 'monto' },
                ],    
                search: '',
                editedIndex: -1,
                id:'',
                Areas:[],
                Regionales:[],
                Empresas:[],
                TipoGastos:[],
                EspecifGastos:[],
                TipoSolicitudes:[],
                Monedas:[],
                ModoPagos:[],
                Proyectos:[],
                EspecifGastos:[],
                Cuentas:[],
                Aprobadores:[],
                Contadores:[],
                valida: 0,
                validaMensaje:[],
                adIdTipoGasto: '',
                idusuario:'',
                nombreusuario:'',
                idarea:'',
                idregional:'',
                idempresa:'',
                idtipogasto:'',
                idespecifgasto:'',
                idtiposolicitud:'',
                idmoneda:'',
                idmodopago:'',
                idproyecto:'',
                factura: false,
                recibo: false,
                idcuenta:'',
                fechasolicitada:'',
                fechaprogramada:'',
                concepto:'',
                idaprobador:'',
                idcontador:'',                

                detalle:'',
                nrodocumento:0,
                monto:0,
                total:0,
                searchcuentas: null,
                codigobanco:'',
                idaddespgasto:'',
                diaGastos:[],
                adiddetalles:'',
                verOrdenpago:0,
                menu: false,
                modal: false,
                menu2: false,
            }
        },
        computed: {
             formTitle () {
            return this.editedIndex === -1 ? 'La solicitud ha sido Aprobada' : 'La solicitud ha sido rechazada'
            },
            calcularTotal:function(){
                var resultado=0.0;
                for(var i=0;i<this.detalles.length;i++){
                    resultado=resultado+(this.detalles[i].monto*1);
                    //this.detalles[i].monto.push(Number(this.detalles[i].monto));
                }
                return resultado;
            }
        },

        watch: {
            dialog (val) {
            val || this.close()
            }
        },

        created () {           
            this.listar();
            this.Select();
        },
        methods:{

            listar(){
                var adidusuario=this.$store.state.usuario.idusuario;
                let me=this;

                axios.get('api/Ordendepagos/Poraprobar/'+adidusuario,{}).then(function(response)
                {
                    //console.log(response);
                    me.ordendepagos=response.data;
                }).catch(function(error){
                    console.log(error);
                });
            },

            listardetalle(iddetalles){
                let me=this;
                me.detalles=[];
                this.adiddetalles=iddetalles;
                axios.get('api/Detalleordenes/Listar/'+this.adiddetalles,{}).then(function(response)
                {
                    //console.log(response);
                    me.detalles=response.data;
                }).catch(function(error){
                    console.log(error);
                });
            },

           Select(){
                let me=this;
                var AreasArray=[];
                axios.get('api/Areas/Select').then(function(response)
                {
                    //console.log(response);
                    AreasArray=response.data;
                    AreasArray.map(function(x){
                        me.Areas.push({text: x.nombre,value:x.idarea});
                    });
                }).catch(function(error){
                    console.log(error);
                });

                var RegionalesArray=[];
                axios.get('api/Regionales/Select').then(function(response)
                {
                    //console.log(response);
                    RegionalesArray=response.data;
                    RegionalesArray.map(function(x){
                        me.Regionales.push({text: x.nombre,value:x.idregional});
                    });
                }).catch(function(error){
                    console.log(error);
                });

                var EmpresasArray=[];
                axios.get('api/Empresas/Select').then(function(response)
                {
                    //console.log(response);
                    EmpresasArray=response.data;
                    EmpresasArray.map(function(x){
                        me.Empresas.push({text: x.nombre,value:x.idempresa});
                    });
                }).catch(function(error){
                    console.log(error);
                });

                var MonedasArray=[];
                axios.get('api/Monedas/Select').then(function(response)
                {
                    //console.log(response);
                    MonedasArray=response.data;
                    MonedasArray.map(function(x){
                        me.Monedas.push({text: x.nombre,value:x.idmoneda});
                    });
                }).catch(function(error){
                    console.log(error);
                });

                var ModoPagosArray=[];
                axios.get('api/Modopagos/Select').then(function(response)
                {
                    //console.log(response);
                    ModoPagosArray=response.data;
                    ModoPagosArray.map(function(x){
                        me.ModoPagos.push({text: x.nombre,value:x.idmodopago});
                    });
                }).catch(function(error){
                    console.log(error);
                });
                var ProyectosArray=[];
                axios.get('api/Proyectos/Select').then(function(response)
                {
                    //console.log(response);
                    ProyectosArray=response.data;
                    ProyectosArray.map(function(x){
                        me.Proyectos.push({text: x.nombre,value:x.idproyecto});
                    });
                }).catch(function(error){
                    console.log(error);
                });
                var TipoSolicitudesArray=[];
                axios.get('api/Tiposolicitudes/Select').then(function(response)
                {
                    //console.log(response);
                    TipoSolicitudesArray=response.data;
                    TipoSolicitudesArray.map(function(x){
                        me.TipoSolicitudes.push({text: x.nombre,value:x.idtiposolicitud});
                    });
                }).catch(function(error){
                    console.log(error);
                });
                var TipoGastosArray=[];
                axios.get('api/TipoGastos/Select').then(function(response)
                {
                    //console.log(response);
                    TipoGastosArray=response.data;
                    TipoGastosArray.map(function(x){
                        me.TipoGastos.push({text: x.nombre,value:x.idtipogasto});
                    });
                }).catch(function(error){
                    console.log(error);
                });

                var CuentasArray=[];
         
                axios.get('api/Cuentas/Select').then(function(response)
                {
                    //console.log(response);
                    CuentasArray=response.data;
                    CuentasArray.map(function(x){
                        me.Cuentas.push({text: x.cuenta+' - '+x.nombre+' - '+x.banco,value:x.idcuenta});
                    });
                    //me.codigobanco=Cuentas.text2;
                }).catch(function(error){
                    console.log(error);
                });

                var AprobadoresArray=[];
         
                axios.get('api/Usuarios/Selectaprobador').then(function(response)
                {
                    //console.log(response);
                    AprobadoresArray=response.data;
                    AprobadoresArray.map(function(x){
                        me.Aprobadores.push({text: x.nombre,value:x.idusuario});
                    });
                    //console.log(me.Aprobadores)
                }).catch(function(error){
                    console.log(error);
                });
                var ContadoresArray=[];        
                axios.get('api/Usuarios/Selectcontador').then(function(response)
                {
                    //console.log(response);
                    ContadoresArray=response.data;
                    ContadoresArray.map(function(x){
                        me.Contadores.push({text: x.nombre,value:x.idusuario});
                    });
                    //console.log(me.Aprobadores)
                }).catch(function(error){
                    console.log(error);
                });
                     
            },

            Selectdinamic(idtipogasto){
                let me=this;
                me.idespecifgasto='';
                me.EspecifGastos=[];
                this.adIdTipoGasto=idtipogasto;
                var EspecifGastosArray=[];
                axios.get('api/Especifgastos/Select/'+this.adIdTipoGasto,{}).then(function(response)
                { 
                    //console.log(response);
                    EspecifGastosArray=response.data;
                    EspecifGastosArray.map(function(x){
                        me.EspecifGastos.push({text: x.nombre,value:x.idespecifgasto});
                    });
                }).catch(function(error){
                    console.log(error);
                });
            },

           agregarDetalle(){
               this.detalles.push(
                   {detalle: "",nrodocumento:"",monto:0
                   }
               );
            },

            eliminardetalle(arr,item){
                var i=arr.indexOf(item);
                if(i!==-1){
                    arr.splice(i,1)
                }
            },

            volver(){
                this.verOrdenpago=0;
                this.validaMensaje=[];
            },

            convertirnumero(){
                for(var i=0;i<this.detalles.length;i++){
                  this.detalles[i].monto=(this.detalles[i].monto)*1;
                }
            },

           
            editItem (item) {
                this.validaMensaje=[];
                this.verOrdenpago=1;
                this.id=item.idordendepago;
                this.idusuario=item.idusuario;
                this.nombreusuario=item.usuario;
                this.idarea=item.idarea;
                this.idregional=item.idregional;
                this.idempresa=item.idempresa;
                this.fechasolicitada=item.fechasolicitud;
                this.fechaprogramada=item.fechaprogramada
                this.idtipogasto=item.idtipogasto;
                this.idespecifgasto=item.idespecifgasto;
                this.idtiposolicitud=item.idtiposolicitud;
                this.idmoneda=item.idmoneda;
                this.idmodopago=item.idmodopago;
                this.idproyecto=item.idproyecto;
                this.factura=item.factura;
                this.recibo=item.recibo;
                this.editedIndex=1;
                this.concepto=item.concepto;
                this.idcuenta=item.idcuenta;
                this.idaprobador=item.idaprobador;
                this.idcontador=item.idcontador;

                let me=this;
                this.Selectdinamic(me.idtipogasto);
                this.idespecifgasto=item.idespecifgasto;
                this.listardetalle(me.id);
            },

            close () {
                this.dialogAprobador=false;
                this.dialog = false;
                this.volver();
                this.listar();
                this.editedIndex=-1;
            },

            aprobar(estado){
                this.convertirnumero();
                
                let me=this;
                me.total=me.total*1
                if(estado==1)
                {  
                    if(this.validar()){
                        return;
                    }
                    axios.put('api/Ordendepagos/Actualizar',{
                        'idordendepago':me.id,
                        'idestado':2,
                        'idusuario':me.idusuario,
                        'idaprobador':me.idaprobador,
                        'idcontador':me.idcontador,
                        'idregional':me.idregional,
                        'idarea':me.idarea,
                        'idempresa':me.idempresa,
                        'idespecifgasto':me.idespecifgasto,
                        'idmoneda':me.idmoneda,
                        'idtiposolicitud':me.idtiposolicitud,
                        'idmodopago':me.idmodopago,
                        'idproyecto':me.idproyecto,
                        'idcuenta':me.idcuenta,
                        'fechaprogramada':me.fechaprogramada,
                        'factura':me.factura,
                        'recibo':me.recibo,
                        'concepto':me.concepto,
                        'total':me.total,
                        'detalleorden':me.detalles

                    }).then(function(response){            
                        me.dialog=true;
                        me.editedIndex=-1; 
                        this.validaMensaje=[];       
                    }).catch(function(error){
                        console.log(error);
                    })
                }
                if(estado==0)
                {
                    this.adId=me.id;
                    axios.put('api/Ordendepagos/Rechazar/'+this.adId,{
                    }).then(function(response){            
                        me.dialog=true;
                        me.editedIndex=1;
                            
                    }).catch(function(error){
                        console.log(error);
                    })
                }
            },

            cambiarAprobador(){
                let me=this;
                axios.put('api/Ordendepagos/Cambiaraprobador',{
                        'idordendepago':me.id,
                        'idaprobador':me.idaprobador,
                    }).then(function(response){            
                        me.dialogAprobador=true;
                        this.validaMensaje=[];        
                    }).catch(function(error){
                        console.log(error);
                    })
            },

            verDialog(){
                this.dialog=true;
            },
            validar(){
                this.valida=0;
                this.validaMensaje=[];
                if(!this.idarea){
                    this.validaMensaje.push("Seleccione un area.")
                }
                if(!this.idregional){
                    this.validaMensaje.push("Seleccione un regional.")
                }
                if(!this.idempresa){
                    this.validaMensaje.push("Seleccione una empresa.")
                }
                if(!this.idtipogasto){
                    this.validaMensaje.push("Seleccione un tipo de gasto.")
                }
                if(!this.idespecifgasto){
                    this.validaMensaje.push("Seleccione una especificacion de gasto.")
                }
                if(!this.idtiposolicitud){
                    this.validaMensaje.push("Seleccione un tipo de solicitud.")
                }
                if(!this.idmoneda){
                    this.validaMensaje.push("Seleccione moneda.")
                }
                if(!this.idmodopago){
                    this.validaMensaje.push("Seleccione un modo de pago.")
                }
                if(!this.idcuenta){
                    this.validaMensaje.push("Seleccione una cuenta.")
                }
                if(this.concepto.length<3 || this.concepto.length>256){
                    this.validaMensaje.push("Escriba el concepto en menos de 256 caracteres.")
                }
                if(!this.idaprobador){
                    this.validaMensaje.push("Seleccione un aprobador.")
                }
                if(this.total==0){
                    this.validaMensaje.push("debe agregar detalles")
                }
                
                for(var i=0;i<this.detalles.length;i++){
                    if(this.detalles[i].monto==0){
                        this.validaMensaje.push("los importes no deben ser igual a 0.");
                    }
                }
                for(var i=0;i<this.detalles.length;i++){
                    if(this.detalles[i].detalle==""){
                        this.validaMensaje.push("falta detalle(s).");
                    }
                } 

                if(this.validaMensaje.length){
                    this.valida=1;
                }
               
                return this.valida;
            },

            activarDesactivarMostrar(accion,item){
                this.adModal=1;
                this.adNombre=item.nombre;
                this.adId=item.idcuenta;
                if (accion==1){
                    this.adAccion=1;

                }
                else if(accion==2){
                    this.adAccion=2;
                }
                else{
                    this.adModal=0;
                }
            },

          
        }        
    }
</script>
