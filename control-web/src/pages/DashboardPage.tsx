import { useEffect } from 'react';
import { useTotalsStore } from '../stores/totalsStore';
import { Table, Card, Row, Col, Statistic, Typography } from 'antd';
import { ArrowUpOutlined, ArrowDownOutlined, DollarOutlined } from '@ant-design/icons';

const { Title } = Typography;

export default function DashboardPage() {
  const { report, isLoading, fetchTotals } = useTotalsStore();

  useEffect(() => {
    fetchTotals();
  }, [fetchTotals]);

  const colums = [
    { title: 'Pessoa', dataIndex: 'name', key: 'name' },
    {
        title: 'Receitas', dataIndex: 'totalIncomes', key: 'totalIncomes',
        render: (val: number) => <span style={{ color: '#3f8600' }}>R$ {val.toFixed(2)}</span>
    },
    { 
      title: 'Despesas', dataIndex: 'totalExpenses', key: 'totalExpenses',
      render: (val: number) => <span style={{ color: '#cf1322' }}>R$ {val.toFixed(2)}</span>
    },
    { 
      title: 'Saldo Líquido', dataIndex: 'balance', key: 'balance',
      render: (val: number) => (
        <strong style={{ color: val >= 0 ? '#1677ff' : '#cf1322' }}>R$ {val.toFixed(2)}</strong>
      )
    },
  ]

  return (
    <div>
      <Title level={2}>Resumo Financeiro</Title>

        {report && (
            <Row gutter={16} style={{ marginBottom: 24 }}>
                <Col span={8}>
                    <Card>
                        <Statistic title="Total Receitas" value={report.generalTotalIncomes} precision={2} valueStyle={{ color: '#3f8600' }} prefix={<ArrowUpOutlined />} suffix="R$" />
                    </Card>
                </Col>
                <Col span={8}>
                    <Card bordered={false}>
                    <Statistic title="Total Despesas" value={report.generalTotalExpenses} precision={2} valueStyle={{ color: '#cf1322' }} prefix={<ArrowDownOutlined />} suffix="R$" />
                    </Card>
                </Col>
                <Col span={8}>
                    <Card bordered={false}>
                    <Statistic title="Saldo Geral" value={report.generalBalance} precision={2} valueStyle={{ color: report.generalBalance >= 0 ? '#1677ff' : '#cf1322' }} prefix={<DollarOutlined />} suffix="R$" />
                    </Card>
                </Col>
            </Row>
        )}
      
      <Table 
        dataSource={report?.items} 
        columns={colums} 
        rowKey="userId" 
        loading={isLoading} 
        pagination={false}
      />
    </div>
  );
}